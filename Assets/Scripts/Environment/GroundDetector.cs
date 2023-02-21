using UnityEngine;

[RequireComponent(typeof(PlayerJump))]
public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _distanceToGround = 0.5f;

    private PlayerJump _playerJump;

    private bool _isGrounded;
    private bool _isJumping;

    private void Start()
    {
        _playerJump = GetComponent<PlayerJump>();

        CheckGround();
    }

    public bool CheckGround()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector2.down, _distanceToGround, _groundMask);

        if (_isGrounded)
            _isJumping = false;

        _playerJump.UpdateGroundedAndJumpingState(_isGrounded, _isJumping);

        return _isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _distanceToGround);
    }
}