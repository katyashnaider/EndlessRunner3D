using System.Collections;
using UnityEngine;

public class PlayerRollOver : MonoBehaviour
{
    [SerializeField] private CapsuleCollider _standartCollider;
    [SerializeField] private CapsuleCollider _colliderJumping;

    private void Start()
    {
        _colliderJumping.enabled = false;
        _standartCollider.enabled = true;
    }

    public IEnumerator RollOver()
    {
        _colliderJumping.enabled = true;
        _standartCollider.enabled = false;

        yield return new WaitForSeconds(1);

        _colliderJumping.enabled = false;
        _standartCollider.enabled = true;
    }
}