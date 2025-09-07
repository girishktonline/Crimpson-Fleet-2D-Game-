using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int totalCoins, totalDiamonds, score;
    private void Awake()
    {
        instance = this;
    }

    public TextMeshProUGUI scoreText, coinText, diamondText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setText();
    }
    public void setText()
    {
       totalCoins = PlayerPrefs.GetInt("PlayerCoin");
       totalDiamonds = PlayerPrefs.GetInt("PlayerDiamond");
       score = PlayerPrefs.GetInt("PlayerScore");

        if(totalCoins <= 99999)
        {
            coinText.text = totalCoins.ToString("D5");
        }
        else
        {
            coinText.text = totalCoins.ToString();
        }

        if(totalDiamonds <= 99999)
        {
            diamondText.text = totalDiamonds.ToString("D5");
        }
        else
        {
            diamondText.text = totalDiamonds.ToString();
        }
        if(score <= 99999)
        {
            scoreText.text = score.ToString("D5");
        }
        else
        {
            scoreText.text = score.ToString();
        }
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseCoin()
    {
        int tempCoin = totalCoins + 5;
        PlayerPrefs.SetInt("PlayerCoin", tempCoin);
        setText();
    }
    public void IncreaseDiamond()
    {
        int tempDiamond = totalDiamonds + 5;
        PlayerPrefs.SetInt("PlayerDiamond", tempDiamond);
        setText();
    }
}
