using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static int score;

    Text text;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
            
        text = GetComponent<Text>();
        score = 0;
    }

    void Update()
    {
        text.text = "Score: " + score;
    }
    
    public int GetScore()
    {
        return score;
    }
    
    public void ResetScore()
    {
        score = 0;
    }
}