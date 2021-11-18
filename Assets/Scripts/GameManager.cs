using Obstacles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Wall> walls;

    [SerializeField] int freezeTime = 3;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        walls = new List<Wall>();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FreezeWalls()
    {
        foreach(Wall wall in walls)
        {
            wall.Freeze(freezeTime);
        }
    }
}
