using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> scoreEntries = new List<Transform>();
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreContainer");
        entryTemplate = transform.Find("HighScoreContainer/ScoreTemplate");

        entryTemplate.gameObject.SetActive(false);
    }

    public void ShowScores(int[] scores)
    {
        RemoveScoreEntries();

        float templateHeight = 50f;
        for (int i = 0; i < scores.Length; i++)
        {
            string rankString = GetRankString(i);
            CreateAndSetScoreEntry(scores, templateHeight, i, rankString);
        }
    }

    private void CreateAndSetScoreEntry(int[] scores, float templateHeight, int i, string rankString)
    {
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        scoreEntries.Add(entryTransform);

        RectTransform entryReactTransform = entryTransform.GetComponent<RectTransform>();
        entryReactTransform.anchoredPosition = new Vector2(0, -templateHeight * (i + 1));

        entryTransform.gameObject.SetActive(true);
        entryTransform.Find("Pos").GetComponent<TMP_Text>().text = rankString;
        entryTransform.Find("Score").GetComponent<TMP_Text>().text = scores[i].ToString();
    }

    private static string GetRankString(int i)
    {
        string rankString;
        int rank = i + 1;
        switch (rank)
        {
            case 1: rankString = "1st"; break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd"; break;
            default:
                rankString = rank + "th"; break;
        }

        return rankString;
    }

    private void RemoveScoreEntries()
    {
        foreach (var scoreEntry in scoreEntries)
        {
            Destroy(scoreEntry.gameObject);
        }

        scoreEntries.Clear();
    }
}
