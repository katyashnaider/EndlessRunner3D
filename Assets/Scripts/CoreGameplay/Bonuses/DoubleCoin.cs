using UnityEngine;

public class DoubleCoin : MonoBehaviour
{
    [SerializeField] private PlayerBonuses _playerBonuses;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out BonusDoubleCoin bonus))
        {
            _playerBonuses.EnableDoublePrice();
            bonus.gameObject.SetActive(false);
        }
    }
}
