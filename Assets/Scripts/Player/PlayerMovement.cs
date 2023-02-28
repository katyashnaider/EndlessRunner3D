using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationSpeed;
    [SerializeField] private int _maxSpeed = 3000;
    [SerializeField] private float _speed = 1000;
    [SerializeField] private float _accelerationRate = 100;
    [SerializeField] private float _accelerationDelay = 3;

    private const string Parametr = "MoveSpeed";

    private Animator _animator;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;

    public event Action<float> SpeedChanged;

    private float Multiplier => _animationSpeed.Evaluate(Timer.PlayTime);
    public float Speed
    {
        get { return _speed; }
        private set
        {
            _speed = value;
            SpeedChanged?.Invoke(_speed);
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        StartCoroutine(Accelerate());
    }

    private void FixedUpdate()
    {
        _moveDirection.z = _speed;
        Move(_moveDirection);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void Move(Vector3 direction)
    {
        direction = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, direction.z * Time.fixedDeltaTime);
        _rigidbody.velocity = direction;
        //_player.transform.Translate(direction * _speed * Time.fixedDeltaTime);
    }

    private IEnumerator Accelerate()
    {
        var wait = new WaitForSeconds(_accelerationDelay);

        while (_speed <= _maxSpeed)
        {
            _speed += _accelerationRate;
            _animator.SetFloat(Parametr, Multiplier);
            yield return wait;
        }
    }
}