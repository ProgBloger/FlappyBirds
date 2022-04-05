using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int[] scores = new int[MAX_SCORES];
    private const string  SCORES_STRING = "MyScores";
    private const string  SCORES_SEPARATOR = ",";
    private const int MAX_SCORES = 5;

    // Start is called before the first frame update
    void Awake()
    {
        LoadScores();
    }

    public void LoadScores()
    {
        var scoresString = PlayerPrefs.GetString(SCORES_STRING);
        var scoresStrArray = scoresString.Split(SCORES_SEPARATOR[0]);
        
        if(scoresStrArray.Length != MAX_SCORES)
        {
            scores = new int[MAX_SCORES];
            return;
        }

        for(int i = 0; i < MAX_SCORES; i++)
        {
            if(!Int32.TryParse(scoresStrArray[i], out scores[i]))
            {
                scores[i] = 0;
            }
        }
    }

    public void SaveScores()
    {
        var sb = new StringBuilder();
        string prefix = string.Empty;

        for(int i = 0; i < MAX_SCORES; i++)
        {
            sb.Append(prefix);
            prefix = SCORES_SEPARATOR;
            sb.Append(scores[i]);
        }

        PlayerPrefs.SetString(SCORES_STRING, sb.ToString());
    }

    public bool UpdateScores(int score)
    {
        bool isNewScoreSwitcher = false;
        int temp = 0;
        int tempForTemp = 0;
        for(int i = 0; i < MAX_SCORES; i++)
        {
            if(isNewScoreSwitcher)
            {
                tempForTemp = scores[i];
                scores[i] = temp; 
                temp = tempForTemp;
            }

            if(score > scores[i] && !isNewScoreSwitcher)
            {
                isNewScoreSwitcher = true;
                temp = scores[i];
                scores[i] = score;
            }
        }

        SaveScores();

        return isNewScoreSwitcher;
    }

    public int[] GetScores()
    {
        return scores;
    }
}
