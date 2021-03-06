
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
    [SerializeField] private int failDelay;

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
        maxLevelIndex = SceneManager.sceneCountInBuildSettings - 1;
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
        playerUI.SetupLivesImage(lives);
        playerUI.ManageLivesImage(lives);
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
    public void LevelFailed(int seconds)
    {
        this.isSuccess = false;
        StartCoroutine(PostGameDelay(seconds));
    }

    IEnumerator PostGameDelay(int seconds)
    {
        GameManager.gameStarted = false;
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
            playerUI.ManageLivesImage(lives);
            if (lives == 0)
            {
                LevelFailed(failDelay);
            }
        }
    }
    public void SetLives(int lives)
    {
        this.lives = lives;
        defaultLive = lives;
        playerUI.ManageLivesImage(lives);
    }
    public void ResetProgress()
    {
        this.progress = 0;
        isSuccess = false;
        this.lives = defaultLive;
        playerUI.SetLivesText(this.lives);
    }
}
