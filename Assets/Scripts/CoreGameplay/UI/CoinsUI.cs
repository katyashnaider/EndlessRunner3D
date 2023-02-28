using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.CoinsAmountChanged += OnCoinsAmountChanged;
    }

    private void OnDisable()
    {
        _player.CoinsAmountChanged -= OnCoinsAmountChanged;
    }

    private void OnCoinsAmountChanged(int coins)
    {
        _coinsText.text = coins.ToString();
    }
}

