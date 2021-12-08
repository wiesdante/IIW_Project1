using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndChecker : MonoBehaviour
{

    ScoreManager scoreMan;

    private void Start()
    {
        scoreMan = ScoreManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (scoreMan.GetScore() >= scoreMan.requiredScore)
            {
                LevelManager.Instance.IncreaseProgress(1);
            } else
            {
                LevelManager.Instance.LevelFailed(0);
            }
        }
    }
}
