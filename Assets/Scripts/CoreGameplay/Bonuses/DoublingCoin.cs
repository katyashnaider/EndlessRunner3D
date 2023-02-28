using UnityEngine;

public class DoublingCoin : MonoBehaviour
{
    [SerializeField] private PlayerBonuses _playerBonuses;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out BonusDoublingCoin bonus))
        {
            _playerBonuses.EnableDoublePrice();
        }
    }
}
