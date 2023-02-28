using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerRollOver : MonoBehaviour
{
    [SerializeField] private Collider _standartCollider;
    [SerializeField] private Collider _colliderJumping;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _colliderJumping.enabled = false;
        _standartCollider.enabled = true;
    }

    public IEnumerator RollOver()
    {
        _animator.Play("RollOver", 0, 0);

        _colliderJumping.enabled = true;
        _standartCollider.enabled = false;


        yield return new WaitForSeconds(1);

        _colliderJumping.enabled = false;
        _standartCollider.enabled = true;
    }
}