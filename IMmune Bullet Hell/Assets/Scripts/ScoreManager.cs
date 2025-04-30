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
        // Reset the score when game starts
        currentScore = 0;
        score = 0;
        UpdateScoreText();
    }

    
    public int GetCurrentScore()
    {
        return currentScore;
    }

    
    public void AddScore(int points)
    {
        currentScore += points;
        score = currentScore; // Keep static variable updated
        UpdateScoreText();
    }

    

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;
    }
}