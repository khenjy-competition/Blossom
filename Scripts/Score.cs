using System.Collections;
using UnityEngine;

[System.Serializable]
public class Score
{
    public float timer { get; set; }
    public string stimer 
    { 
        get 
        { 
            return string.Format("{00:00}:{01:00}", Mathf.FloorToInt((timer + 1) / 60), Mathf.FloorToInt(timer % 60));
        }
    }
    public int score { get; set; }
    public int bomb { get; set; }

    public int maxScore { get; private set; }
    public float maxTime { get; private set; }

    public void Initialize()
    {
        maxScore = 5;
        maxTime = 90;
    }
}