using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _speed = 3;
    [SerializeField] private float _jumpForce = 3; //возможно нужно будет убрать
    [SerializeField] private float _stepPosition = 1;

    [SerializeField] private SwipeController _swipeController;
    [SerializeField] private Transform[] _roadPoints;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    private Vector3 _moveDirection;

    private int _index = 1;
    private int _shiftLeft = 0;
    private int _shiftRight = 2;

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
    }

    private void FixedUpdate()
    {
        _moveDirection.z = _speed;
        Move(_moveDirection);
    }

    private void Jump()
    {
        _moveDirection.y = _jumpForce;
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

        if (direction == Vector2.up)
        {
            //if (_characterController.isGrounded)
            //Jump();
        }

        _coroutine = StartCoroutine(ChangeRoad(_roadPoints[_index].position.x, _stepPosition));
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
}
