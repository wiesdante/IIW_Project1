
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    
    [SerializeField] public int lives = 1;
    private float maxLevelIndex;
    [SerializeField] private int complete;

    private PlayerUIHandler playerUI;
    private GameManager gm;
    private int defaultLive , level, progress;
    private bool isSuccess;

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        defaultLive = lives;
        isSuccess = false;
        progress = 0;
        complete = 0;
        maxLevelIndex = SceneManager.sceneCount - 1;
    }
    public void SetComplete(int complete)
    {
        this.complete = complete;
    }
    public void IncreaseComplete(int increase)
    {
        this.complete += increase;
    }
    void Start()
    {
        gm = GameManager.Instance;
        playerUI = PlayerUIHandler.Instance;
        level = PlayerPrefs.GetInt("Level", 0);
    }
    public void StartLevel()
    {
        ResetProgress();
        //StartCoroutine(playerUI.Countdown(3));
        GameManager.gameStarted = true;
    }
    public bool LevelFinished()
    {
        if (this.progress >= complete)
        {
            return true;
        }
        else return false;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(level);
    }
    public void LevelFailed()
    {
        this.isSuccess = false;
        StartCoroutine(PostGameDelay(3));
    }

    IEnumerator PostGameDelay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        gm.SetPhase(Phase.POSTGAME);
    }

    public bool LevelStatus()
    {
        return this.isSuccess;
    }
    public void IncreaseProgress(int progress)
    {
        this.progress += progress;
        if (LevelFinished())
        {
            this.isSuccess = true;
            level++;
            if(level > maxLevelIndex)
            {
                level = 0;
            }
            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.Save();
            gm.SetPhase(Phase.POSTGAME);
        }
    }
    public void DecreaseLife()
    {
        if(!isSuccess)
        {
            lives--;
            playerUI.SetLivesText(lives);
            if (lives == 0)
            {
                LevelFailed();
            }
        }
    }
    public void SetLives(int lives)
    {
        this.lives = lives;
        defaultLive = lives;
    }
    public void ResetProgress()
    {
        this.progress = 0;
        isSuccess = false;
        this.lives = defaultLive;
        playerUI.SetLivesText(this.lives);
    }

}
