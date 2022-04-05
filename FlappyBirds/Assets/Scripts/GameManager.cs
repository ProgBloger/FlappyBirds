using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] HighScoreTable highScoreTable;
    public TMP_Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public Player player;
    private int score;

    private void Start()
    {
        Application.targetFrameRate = 60;
        UpdateScores();

        Pause(true);
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        UpdateUI(false);
        Pause(false);

        IEnumerable<Pipes> pipes = FindObjectsOfType<Pipes>();

        foreach (var pipeSet in pipes)
        {
            Destroy(pipeSet.gameObject);
        }
    }

    public void GameOver()
    {
        var newScoreTable = scoreManager.UpdateScores(score);
        if(newScoreTable)
        {
            UpdateScores();
        }

        UpdateUI(true);

        Pause(true);
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void UpdateScores()
    {
        var scores = scoreManager.GetScores();
        highScoreTable.ShowScores(scores);
    }

    private void UpdateUI(bool turnOn)
    {
        playButton.SetActive(turnOn);
        gameOver.SetActive(turnOn);
        highScoreTable.gameObject.SetActive(turnOn);
    }

    private void Pause(bool setPause)
    {
        Time.timeScale = setPause ? 0f : 1f;
        player.enabled = !setPause;
    }
}
