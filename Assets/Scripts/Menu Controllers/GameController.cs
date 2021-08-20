using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/**
 * PauseGame пауза игры через объявление Time.timeScale = 0, отображение окна паузы и выключние музыки.
 * ResumeGame возобновление игры через Time.timeScale = 1, закрытие окна паузы и выключение музыки.
 * ExitGame выход из сцены игры в меню.
 * GameOver отображение окна окончания игры и выключние музыки.
 * RestartLevel перезапуск уровня.
 * MusicCheck проверка вкл / выкл музыки через получение значения от PlayerPrefs.
 */

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _exitButton2;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        MusicCheck();
        _pausePanel.SetActive(false);
        _pauseButton.onClick.AddListener(PauseGame);
        _exitButton.onClick.AddListener(ExitGame);
        _exitButton2.onClick.AddListener(ExitGame);
        _resumeButton.onClick.AddListener(ResumeGame);
        _restartButton.onClick.AddListener(RestartLevel);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        _pausePanel.SetActive(true);
        _audioSource.mute = true;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
        _audioSource.mute = false;
    }
    private void ExitGame()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Tags.MAIN_MENU);
    }
    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _audioSource.mute = true;
    }
    private void RestartLevel()
    {
        Application.LoadLevel(Tags.GAME_LEVEL);

    }
    private void MusicCheck()
    {
        if(PlayerPrefs.GetInt(Tags.MUSIC_VOLUME)==2)
        {
            _audioSource.mute = true;
        }
    }
}
