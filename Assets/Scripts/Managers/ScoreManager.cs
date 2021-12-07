using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private PlayerUIHandler playerUI;
    
    private int score = 0;

    public int requiredScore = 200;

    public static ScoreManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Start()
    {
        playerUI = PlayerUIHandler.Instance;
        ResetScore();
    }

    public void IncreaseScore(int add)
    {
        if(GameManager.gameStarted)
        {
            score += add;
            UI.ScoreManagerV2.Instance.AddScore(add);
            playerUI.UpdateScore(score);
        }
    }
    public void ResetScore()
    {
        score = 0;
        playerUI.UpdateScore(score);
    }
    public void MultiplyScore(int multiply)
    {
        score *= multiply;
        playerUI.UpdateScore(score);
    }
    public void DivideScore(int divide)
    {
        if(GameManager.gameStarted)
        {
            score /= divide;
            playerUI.UpdateScore(score);
        }
    }
    public int GetScore()
    {
        return this.score;
    }
}
