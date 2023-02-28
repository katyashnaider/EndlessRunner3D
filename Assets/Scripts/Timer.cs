using UnityEngine;

public static class Timer
{
    private static float _sessionStartTime = 0;

    public static float PlayTime => Time.time - _sessionStartTime; 

    public static void OnGameRestart()
    {
        _sessionStartTime = Time.time;
    }
}