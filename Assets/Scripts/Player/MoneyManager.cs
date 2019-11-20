using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Handles a player's money management
public class MoneyManager : MonoBehaviour
{

    public Text moneyText;
    public int currentGold;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("CurrentMoney", 0);
        if (PlayerPrefs.HasKey("CurrentMoney"))
        {
            currentGold = PlayerPrefs.GetInt("CurrentMoney");
        }
        else
        {
            currentGold = 0;
            PlayerPrefs.SetInt("CurrentMoney", 0);
        }

        moneyText.text = "Gold: " + currentGold;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMoney(int goldToAdd)
    {
        currentGold += goldToAdd;
        PlayerPrefs.SetInt("CurrentMoney", currentGold);
        moneyText.text = "Gold: " + currentGold;
    }
}
