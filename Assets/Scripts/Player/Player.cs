using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * UpdateScore Обновляет очки игрока черех подписку на событие ScoreChanged в классе Matrix.
 */

public class Player : MonoBehaviour
{
    [SerializeField] private Text _playerScore;
    private int _score = 0;
  private void Start()
    {
        _playerScore.text = _score.ToString();
    }
    public void UpdateScore(int score)
    {
        _score += score;
        _playerScore.text = _score.ToString();
        if(PlayerPrefs.GetInt(Tags.PLAYER_SCORE)<_score)
        {
            PlayerPrefs.SetInt(Tags.PLAYER_SCORE, _score);
        }
    }
    private void OnEnable()
    {
        Matrix.ScoreChanged += UpdateScore;
    }
    private void OnDisable()
    {
        Matrix.ScoreChanged -= UpdateScore;
    }
}
