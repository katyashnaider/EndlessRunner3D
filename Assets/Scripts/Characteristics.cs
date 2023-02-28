using System;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Characteristics : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Score _score;

    private const string _coinsKey = "Coins";
    private const string _bestScoreKey = "BestScore";

    public static int Coins { get; private set; }
    public static int BestScore { get; private set; }

    private void Start()
    {
        Coins = GetCoins();
        BestScore = GetBestScore();
    }

    private void OnEnable()
    {
        _player.CoinsAmountChanged += OnCoinsAmountChanged;
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _player.CoinsAmountChanged -= OnCoinsAmountChanged;
        _player.GameOver -= OnGameOver;
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt(_coinsKey);
    }

    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt(_bestScoreKey);
    }

    private void OnCoinsAmountChanged(int coins)
    {
        Coins = coins;

        PlayerPrefs.SetInt(_coinsKey, Coins);
    }

    private void OnGameOver()
    {
        if (_score.ScoreCounter > BestScore)
        {
            BestScore = _score.ScoreCounter;
            PlayerPrefs.SetInt(_bestScoreKey, BestScore);
        }
    }
}

