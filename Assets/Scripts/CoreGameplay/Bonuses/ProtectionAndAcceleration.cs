using System.Collections;
using UnityEngine;

public class ProtectionAndAcceleration : MonoBehaviour
{
    [SerializeField] private PlayerBonuses _player;
    [SerializeField] private PlayerMovement _playerMovement;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out BonusProtectionAndAcceleration bonus))
        {
            _player.EnableProtection();
            _player.EnableAcceleration(_playerMovement.Speed);
            bonus.gameObject.SetActive(false);
        }
    }
}