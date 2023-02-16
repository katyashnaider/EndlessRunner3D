using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed = 3;
    [SerializeField] private float _stepPosition = 1f;
    [SerializeField] private float _duration;
    [SerializeField] private float _groundLenght = 0.5f;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private AnimationCurve _yAnimation;
    [SerializeField] private SwipeController _swipeController;
    [SerializeField] private Transform[] _roadPoints;
    [SerializeField] private CapsuleCollider _standartCollider;
    [SerializeField] private CapsuleCollider _colliderJumping;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;
    private Vector3 _moveDirection;

    private int _index = 1;
    private int _shiftLeft = 0;
    private int _shiftRight = 2;
    private bool _isGround = false;

    private void OnEnable()
    {
        _swipeController.Swiped += OnSwiped;
    }

    private void OnDisable()
    {
        _swipeController.Swiped -= OnSwiped;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        //_colliderJumping.enabled = false;
        //_standartCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        _moveDirection.z = _speed;
        Move(_moveDirection);
    }

    private void Jump(Vector3 direction)
    {
        StartCoroutine(AnimationByTime(_duration));
    }

    private void Move(Vector3 direction)
    {
        direction = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, direction.z * Time.fixedDeltaTime);
        _rigidbody.velocity = direction;
    }

    private void OnSwiped(Vector2 direction)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (direction == Vector2.right)
        {
            if (_index < _shiftRight)
            {
                _index++;
            }
        }

        if (direction == Vector2.left)
        {
            if (_index > _shiftLeft)
            {
                _index--;
            }
        }

        if (direction == Vector2.up && CheckGround())
        {
            Jump(direction);
        }

        if (direction == Vector2.down)
        {
            StartCoroutine(RollOver());
        }

        _coroutine = StartCoroutine(ChangeRoad(_roadPoints[_index].position.x, _stepPosition));
    }

    private bool CheckGround()
    {
        return _isGround = Physics.Raycast(transform.position, Vector2.down, _groundLenght, _groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundLenght);
    }

    private IEnumerator ChangeRoad(float target, float step)
    {
        float x = transform.position.x;

        while (transform.position.x != target)
        {
            x = Mathf.MoveTowards(transform.position.x, target, step);
            Vector3 playerPosition = new Vector3(x, transform.position.y, transform.position.z);
            transform.position = playerPosition;

            yield return null;
        }
    }

    private IEnumerator AnimationByTime(float duration)
    {
        float progress = 0f;
        float expiredSeconds = 0f;
        float offset = transform.position.y;

        while (progress < 1)
        {
            expiredSeconds += Time.deltaTime;
            progress = expiredSeconds / duration;

            Vector3 position = transform.position;
            position.y = _yAnimation.Evaluate(progress) + offset;
            transform.position = position;
            position.y = offset;

            yield return null;
        }
    }

    private IEnumerator RollOver()
    {
        _colliderJumping.enabled = true;
        _standartCollider.enabled = false;

        yield return new WaitForSeconds(1);

        _colliderJumping.enabled = false;
        _standartCollider.enabled = true;
    }
}