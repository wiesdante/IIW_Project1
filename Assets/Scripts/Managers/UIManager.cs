using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    //InGame
    private PlayerUIHandler playerUI;
    private GameManager gm;
    private LevelManager level;
    //PostGame
    [SerializeField] private Button backButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas PostGameCanvas;
    [SerializeField] private Canvas InGameCanvas;

    [SerializeField] private Image postGameScreen;
    [SerializeField] private Sprite winSprite;
    [SerializeField] private Sprite loseSprite;

    private Canvas activeCanvas;


    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }
    }
    private void Start()
    {
        gm = GameManager.Instance;
        level = LevelManager.Instance;
        playerUI = PlayerUIHandler.Instance;
        backButton.onClick.AddListener(Back);
        restartButton.onClick.AddListener(RestartGame);
        startButton.onClick.AddListener(StartGame);
        nextLevelButton.onClick.AddListener(PassLevel);
        restartButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        ResetCanvas();
        SetInterface(Phase.MENU);
    }
    private void RestartGame()
    {
        level.RestartLevel();
    }
    private void PassLevel()
    {
        level.NextLevel();
    }
    private void StartGame()
    {
        gm.SetPhase(Phase.INGAME);
    }
    private void Back()
    {
        gm.SetPhase(Phase.MENU);
    }
    private void ResetCanvas()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        InGameCanvas.gameObject.SetActive(false);
        PostGameCanvas.gameObject.SetActive(false);
    }
    public void SetInterface(Phase phase)
    {
        int delay = 0;
        if(activeCanvas != null)
        {
            activeCanvas.gameObject.SetActive(false);
        }
        switch (phase)
        {
            case Phase.MENU:
                activeCanvas = mainMenuCanvas;
                break;
            case Phase.INGAME:
                activeCanvas = InGameCanvas;
                break;
            case Phase.POSTGAME:
                activeCanvas = PostGameCanvas;
                if (level.LevelStatus())
                {
                    postGameScreen.sprite = winSprite;
                    playerUI.setInfoText("Score\n"+ScoreManager.Instance.GetScore().ToString());
                    nextLevelButton.gameObject.SetActive(true);
                }
                else
                {
                    postGameScreen.sprite = loseSprite;
                    playerUI.setInfoText("Score\n" + ScoreManager.Instance.GetScore().ToString());
                    delay = 0;
                    restartButton.gameObject.SetActive(true);
                }
                break;
        }
        StartCoroutine(DelayedAction(delay));
    }

    IEnumerator DelayedAction(int delay)
    {
        if(delay > 0) yield return new WaitForSeconds(delay);
        activeCanvas.gameObject.SetActive(true);
    }
}
