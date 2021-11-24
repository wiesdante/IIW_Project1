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
    [SerializeField] private Slider ballSlider;
    [SerializeField] private Image strikeImage;

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
    public void DecreaseLivesText()
    {
        ballSlider.value += 1;
    }
    public void ResetLivesText()
    {
        ballSlider.value = 0;
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
}
