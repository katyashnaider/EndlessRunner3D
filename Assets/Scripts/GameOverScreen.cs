using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Button _restartButton;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Player _player;

    private void Start()
    {
        _canvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _player.Died += OnDied;

    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _player.Died -= OnDied;
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(0);
        _canvasGroup.alpha = 1;
        Time.timeScale = 1;
    }

    private void OnDied()
    {
        _canvasGroup.alpha = 1;
        Time.timeScale = 0;
    }
}
