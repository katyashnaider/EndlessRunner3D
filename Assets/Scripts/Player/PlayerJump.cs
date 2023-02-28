using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(Animator))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve _yAnimation;
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _changeHeight = 0.5f;

    private Animator _animator;

    private bool _isGrounded;
    private bool _isJumping;

    private GroundDetector _ground;

    private void Start()
    {
        _ground = GetComponent<GroundDetector>();
        _animator = GetComponent<Animator>();
    }

    public void TryJump()
    {
        if (_ground.CheckGround())
            StartCoroutine(AnimationByTime(transform, _duration));
    }

    public void UpdateGroundedAndJumpingState(bool isGrounded, bool isJumping)
    {
        _isGrounded = isGrounded;
        _isJumping = isJumping;
    }

    private IEnumerator AnimationByTime(Transform playerPosition, float duration)
    {
        float progress = 0f;
        float expiredSeconds = 0f;
        float offset = playerPosition.position.y;
        Vector3 colliderPosition = _collider.center;

        _isGrounded = false;
        _isJumping = true;

        _animator.Play("Jump", 0, 0);
        _collider.center = new Vector3(_collider.center.x, _collider.center.y + _changeHeight, _collider.center.z);

        while (progress < 1)
        {
            expiredSeconds += Time.deltaTime;
            progress = expiredSeconds / duration;

            Vector3 position = playerPosition.position;
            position.y = _yAnimation.Evaluate(progress) + offset;
            playerPosition.position = position;
            position.y = offset;

            yield return null;
        }

        _isGrounded = true;
        _isJumping = false;

        UpdateGroundedAndJumpingState(_isGrounded, _isJumping);

        _collider.center = colliderPosition;
    }
}