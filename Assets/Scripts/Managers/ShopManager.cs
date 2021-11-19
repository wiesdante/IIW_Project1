using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class ShopManager : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private CinemachineVirtualCamera ingameCam;
    [SerializeField] private CinemachineVirtualCamera shopCam;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button backButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button selectButton;
    private int selection;
    private int currency;
    private int cost;

    public static ShopManager Instance { get; private set; }
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

        gm = GameManager.Instance;

        


        selection = 0;
        currency = PlayerPrefs.GetInt("Currency" , 0);
        currencyText.text = this.currency.ToString();
        backButton.onClick.AddListener(Back);
        buyButton.onClick.AddListener(Buy);
        nextButton.onClick.AddListener(NextButtonAction);
        previousButton.onClick.AddListener(PreviousButtonAction);
        selectButton.onClick.AddListener(SelectButtonAction);

    }
    private void SelectButtonAction()
    {

    }
    private void Buy()
    {
        if(currency >= cost)
        {
            currency -= cost;
            currencyText.text = currency.ToString();
            PlayerPrefs.SetInt("Currency", currency);
            PlayerPrefs.Save();
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(ShowInfo());
        }
    }
    private void PreviousButtonAction()
    {

    }
    private void NextButtonAction()
    {

    }
    private void ShowPrice()
    {
        if(selection == 0)
        {
            cost = 0;
        }
        else
        {
            cost = (selection + 1) * 10000;
        }
        priceText.text = cost.ToString();
    }
    private void Back()
    {
        gm.SetPhase(Phase.MENU);
    }
    public void IncreaseCurrency(int add)
    {
        this.currency += add;
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.Save();
        currencyText.text = this.currency.ToString();
    }
    private IEnumerator ShowInfo()
    {
        infoText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        infoText.gameObject.SetActive(false);
    }
}
