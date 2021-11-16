using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public static ScoreManager instance;


    private void Awake()
    {
        //todo load previous score
        score = 0;

        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void AddScore(int score)
    {
        this.score += score;
        UIManager.instance.updateScoreText("Score: " + this.score);
    }
}
