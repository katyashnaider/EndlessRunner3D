using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerCoinCollector : MonoBehaviour
{
    private Player _player;
    private int _multiplierCoins = 1;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Coin coin))
        {
            _player.ApplyCoin(Coin.Amount * _multiplierCoins);
            coin.Disable();
        }
    }

    public void EnableDoubleCoins()
    {
        _multiplierCoins = 2;
    }

    public void DisableDoubleCoins()
    {
        _multiplierCoins = 1;
    }
}