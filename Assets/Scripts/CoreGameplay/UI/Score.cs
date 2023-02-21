using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private TMP_Text _scoreText;

    private void Update()
    {
        _scoreText.text = ((int)(_player.position.z / 2)).ToString();
    }
}
