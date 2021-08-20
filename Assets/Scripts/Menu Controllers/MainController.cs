using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _highScoreText;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _plusSpeed;
    [SerializeField] private Button _minusSpeed;
    [SerializeField] private Text _speedText;
    [SerializeField] private Button _plusMusic;
    [SerializeField] private Button _minusMusic;
    [SerializeField] private Text _musicText;
    private int _speed = 1;
    private void Start()
    {
        GetStartingValue();
        _button.onClick.AddListener(PlayGame);
        _highScoreText.text = PlayerPrefs.GetInt(Tags.PLAYER_SCORE).ToString();
        _optionsButton.onClick.AddListener(OpenOptionsPanel);
        _backButton.onClick.AddListener(CloseOptionsPanel);
        _plusSpeed.onClick.AddListener(() => ChangeDifficulty(1));
        _minusSpeed.onClick.AddListener(() => ChangeDifficulty(-1));
        _plusMusic.onClick.AddListener(() => ChangeMusic(true));
        _minusMusic.onClick.AddListener(() => ChangeMusic(false));
    }
    private void PlayGame()
    {
        Application.LoadLevel(Tags.GAME_LEVEL);
    }
    private void OpenOptionsPanel()
    {
        _optionsPanel.SetActive(true);
    }
    private void CloseOptionsPanel()
    {
        _optionsPanel.SetActive(false);
        PlayerPrefs.SetInt(Tags.GAME_SPEED, _speed);
    }
    private void ChangeDifficulty(int change)
    {
        _speed += change;
       _speed = Mathf.Clamp(_speed, 1, 4);
        _speedText.text = _speed.ToString();
    }
    private void ChangeMusic(bool change)
    {
        if(change== true)
        { _musicText.text = "ON";
            PlayerPrefs.SetInt(Tags.MUSIC_VOLUME, 1);
        }
        else if(change == false)
        { _musicText.text = "OFF";
            PlayerPrefs.SetInt(Tags.MUSIC_VOLUME,2);
        }
    }
    private void GetStartingValue()
    {
        _speed = PlayerPrefs.GetInt(Tags.GAME_SPEED);
        if (PlayerPrefs.GetInt(Tags.GAME_SPEED) == 0)
            _speed = 1;
        _speedText.text = _speed.ToString();

        if (PlayerPrefs.GetInt(Tags.MUSIC_VOLUME) == 1)
        { _musicText.text = "ON"; }
        else { _musicText.text = "OFF";
        }
    }
}
