using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;

    public int ScoreCounter { get; private set; } = 0;

    private void Update()
    {
        ScoreCounter = (int)_player.transform.position.z / 2;
        _scoreText.text = ScoreCounter.ToString() + "/" + Characteristics.BestScore;
    }
}
