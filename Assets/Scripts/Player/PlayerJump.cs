using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GroundDetector))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private AnimationCurve _yAnimation;
    [SerializeField] private float _duration;

    private bool _isGrounded;
    private bool _isJumping;

    private GroundDetector _ground;

    private void Start()
    {
        _ground = GetComponent<GroundDetector>();
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

        _isGrounded = false;
        _isJumping = true;

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
    }
}