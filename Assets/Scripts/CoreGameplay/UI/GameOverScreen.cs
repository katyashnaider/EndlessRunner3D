using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _buttonToMenu;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coinsCountText;

    private string _coinKey = "Coins";

    public event Action GameRestart; 

    private void Start()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        Time.timeScale = 1;
        UpdateCoinsCount();
    }


    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _buttonToMenu.onClick.AddListener(OnToMenu);
        _player.Died += OnDied;

    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _buttonToMenu.onClick.RemoveListener(OnToMenu);
        _player.Died -= OnDied;
    }

    private void OnRestartButtonClick()
    {
        GameRestart?.Invoke();
        UpdateCoinsCount();

        SceneManager.LoadScene(0);
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        Time.timeScale = 1;
    }

    private void OnDied()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        Time.timeScale = 0;
    }

    private void OnToMenu()
    {
        SceneManager.LoadScene(1);
    }

    private void UpdateCoinsCount()
    {
        int coins = PlayerPrefs.GetInt(_coinKey);
        _coinsCountText.text = coins.ToString();
    }
}
