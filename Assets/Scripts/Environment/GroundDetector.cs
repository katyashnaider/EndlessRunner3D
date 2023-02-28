using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _distanceToGround = 0.5f;

    private void Start()
    {
        IsGrounded();
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector2.down, _distanceToGround, _groundMask);      
    }
}