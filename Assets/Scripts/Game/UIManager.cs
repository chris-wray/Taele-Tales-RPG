using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Player HP Bar manager
public class UIManager : MonoBehaviour {

    public Slider healthBar;
    public Text HPText;
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
        HPText.text = "HP: " + healthBar.value + "/" + healthBar.maxValue;
	}

	public void QuitGame(){
		Application.Quit();
	}
}
