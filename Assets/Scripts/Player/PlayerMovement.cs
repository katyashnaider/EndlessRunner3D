using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _speed = 3;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _moveDirection.z = _speed;
        Move(_moveDirection);
    }

    private void Move(Vector3 direction)
    {
        direction = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, direction.z * Time.fixedDeltaTime);
        _rigidbody.velocity = direction;
        //_player.transform.Translate(direction * _speed * Time.fixedDeltaTime);
    }
}