using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameOverScreen _gameOverScreen;

    private static float _sessionStartTime = 0;

    public static float PlayTime => Time.time - _sessionStartTime;

    private void OnEnable()
    {
        _gameOverScreen.GameRestart += OnGameRestart;
    }

    private void OnDisable()
    {
        _gameOverScreen.GameRestart -= OnGameRestart;
    }

    public void OnGameRestart()
    {
        _sessionStartTime = Time.time;
    }
}