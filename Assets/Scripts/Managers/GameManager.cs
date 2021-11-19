using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum Phase { INGAME, SHOP, MENU, POSTGAME }
public class GameManager : MonoBehaviour
{
    //SETUP
    [SerializeField] private CinemachineBrain cameraBrain;

    [SerializeField] private CinemachineVirtualCamera InGameCam;
    [SerializeField] private CinemachineVirtualCamera PostGameCam;
    [SerializeField] private CinemachineVirtualCamera MainMenuCam;
    [SerializeField] private ParticleSystem confetti;
    //Managers
    private LevelManager levelManager;
    private UIManager uiManager;

    //Class variables
    private Phase gamePhase;
    public static bool gameStarted;

    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if( Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameStarted = false;
       
    }
    private void Start()
    {
        uiManager = UIManager.Instance;
        levelManager = LevelManager.Instance;
        InGameCam.gameObject.SetActive(false);
        PostGameCam.gameObject.SetActive(false);
        MainMenuCam.gameObject.SetActive(false);
        SetPhase(Phase.MENU);
    }

    private void SetPhaseAction()
    {
        SetUIAndCamera();
        switch (this.gamePhase)
        {
            case Phase.MENU:
                break;
            case Phase.INGAME:
                ResetAll();
                break;
            case Phase.POSTGAME:
                gameStarted = false;
                break;
        }
    }
    private void ResetAll()
    {
        levelManager.StartLevel();
        //Reload Scene ?? 
    }
    private void SetUIAndCamera()
    {
        var active = cameraBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
        if(active != null)
        {
            active.gameObject.SetActive(false);
        }
        uiManager.SetInterface(gamePhase);
        switch (gamePhase)
        {
            case Phase.INGAME:
                InGameCam.gameObject.SetActive(true);
                break;
            case Phase.MENU:
                MainMenuCam.gameObject.SetActive(true);
                break;
            case Phase.POSTGAME:
                PostGameCam.gameObject.SetActive(true);
                if (levelManager.LevelStatus())
                {
                    StartCoroutine(PlayWinActions());
                }
                else
                {
                    StartCoroutine(PlayLoseActions());
                }
                break;
        }
    }
    private IEnumerator PlayWinActions()
    {
        yield return new WaitForSeconds(2f);
        if(confetti != null)
        {
            confetti.Play();
        }
    }
    private IEnumerator PlayLoseActions()
    {
        yield return null;
    }
    public void ResetFollow()
    {
        PostGameCam.Follow = null;
        PostGameCam.LookAt = null;
    }
    public void SetPhase(Phase phase)
    {
        this.gamePhase = phase;
        SetPhaseAction();
    }
}

