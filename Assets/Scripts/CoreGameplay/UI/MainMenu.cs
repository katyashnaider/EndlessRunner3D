using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playGameButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _coinsCountText;
    [SerializeField] private TMP_Text _bestScoreText;


    private void Start()
    {
        _coinsCountText.text = Characteristics.GetCoins().ToString();
        _bestScoreText.text = Characteristics.GetBestScore().ToString();
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        _playGameButton.onClick.AddListener(OnPlayGameButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _playGameButton.onClick.RemoveListener(OnPlayGameButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnPlayGameButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    private void OnExitButtonClick()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
