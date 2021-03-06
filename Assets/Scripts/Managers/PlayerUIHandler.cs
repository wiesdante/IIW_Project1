using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI postGameScoreText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private Image strikeImage;
    [SerializeField] private GameObject livesStack;
    [SerializeField] private GameObject lifeBallPrefab;

    public bool Instantiated;
    public static PlayerUIHandler Instance { get; private set; }
    private void Awake()
    {
        Instantiated = false;
        if (Instance == null)
        {
            Instance = this;
            Instantiated = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetLivesText(int lives)
    {
        livesText.text = lives.ToString();
    }
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
        postGameScoreText.text = score.ToString();
    }
    public void setInfoText(string text)
    {
       infoText.text = text;
    }
    public IEnumerator Countdown(int seconds)
    {
        countdownText.gameObject.SetActive(true);
        while (seconds > 0)
        {
            countdownText.text = seconds.ToString();
            seconds--;
            yield return new WaitForSeconds(1);
        }
        GameManager.gameStarted = true;
        countdownText.text = "GO !";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }

    public void SetStrikeImg(bool active)
    {
        strikeImage.gameObject.SetActive(active);
    }

    public void SetupLivesImage(int lives)
    {
        for (int i = 0; i<lives; i++)
        {
            GameObject ball = Instantiate(lifeBallPrefab, livesStack.transform);
            ball.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, (float)i /lives);

            ball.SetActive(false);
        }
    }

    public void ManageLivesImage(int lives)
    {
        for(int i = 0; i < livesStack.transform.childCount; i++)
        {
            if(i < lives) livesStack.transform.GetChild(i).gameObject.SetActive(true);
            else livesStack.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
