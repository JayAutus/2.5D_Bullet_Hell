using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
    
    
public class ScoreManager : MonoBehaviour
{
    public static int score;
        
    public TextMeshProUGUI scoreText;   // Drag your ScoreText here

    
    
    private int currentScore = 0;

    void Start()
    {
        UpdateScoreText();
    }

    
    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreText();
    }

    

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore;
    }
}