using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] HighScoreTable highScoreTable;
    [SerializeField] Spawner spawner;
    [SerializeField] BombManager bombManager;
    public TMP_Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject newScoreSign;
    public Player player;
    private int score;

    private void Start()
    {
        Application.targetFrameRate = 60;
        UpdateScores();

        Pause(true);
        SetGameEnterVisible(true);
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        SetGameEnterVisible(false);
        Pause(false);

        spawner.BlowUpPipes();
    }

    public void GameOver()
    {
        bombManager.SetBombInactive();
        SetGameEnterVisible(true);

        var newScoreTable = scoreManager.UpdateScores(score);

        if(newScoreTable)
        {
            UpdateScores();
            newScoreSign.SetActive(true);
        }

        Pause(true);
    }

    public void IncreaseScore()
    {
        score++;
        if(score % 5 == 0)
        {
            bombManager.SetBombActive();
        }
        scoreText.text = score.ToString();
    }

    private void UpdateScores()
    {
        var scores = scoreManager.GetScores();
        highScoreTable.ShowScores(scores);
    }

    private void SetGameEnterVisible(bool turnOn)
    {
        playButton.SetActive(turnOn);
        gameOver.SetActive(turnOn);
        highScoreTable.gameObject.SetActive(turnOn);
        newScoreSign.SetActive(false);
    }

    private void Pause(bool setPause)
    {
        Time.timeScale = setPause ? 0f : 1f;
        player.enabled = !setPause;
    }
}
