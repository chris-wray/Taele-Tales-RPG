using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Player HP Bar manager
public class UIManager : MonoBehaviour {
    public Slider healthBar;
    public Slider manaBar;

    public Text HPText;
    public Text manaText;
    public Text attackText;
    public Text defenseText;

    public PlayerHealthManager playerHealth;

    private static bool UIExists;

	// Use this for initialization
	void Start () {
        if (!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        healthBar.maxValue = playerHealth.playerMaxHealth;
        healthBar.value = playerHealth.playerCurrentHealth;

        manaBar.maxValue = playerHealth.playerMaxMana;
        manaBar.value = playerHealth.playerCurrentMana;

        HPText.text = "HP: " + healthBar.value + "/" + healthBar.maxValue;
        manaText.text = "MP: " + manaBar.value + "/" + manaBar.maxValue;
        attackText.text = "ATK: " + playerHealth.playerCurrentDamage;
        defenseText.text = "DEF: " + playerHealth.playerCurrentDefense;
    }

	public void QuitGame(){
		Application.Quit();
	}
}
