using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the player's health
public class PlayerHealthManager : MonoBehaviour {

    public int playerMaxHealth;
    public int playerCurrentHealth;

    public int playerMaxMana;
    public int playerCurrentMana;

    public int playerCurrentDamage;
    public int playerCurrentDefense;

    private SFXManager sfxMan;

	// Use this for initialization
	void Start () {
        playerCurrentHealth = playerMaxHealth;
        playerCurrentMana = playerMaxMana;
        playerCurrentDamage = 5;
        playerCurrentDefense = 0;
    sfxMan = FindObjectOfType<SFXManager>();
	}
	
	// Update is called once per frame
	void Update () {
		// Check if a player died.
		if(playerCurrentHealth <= 0){
            gameObject.SetActive(false);
        }
	}
    // API used by monsters to damage a player.	
    public void HurtPlayer(int damage){
        playerCurrentHealth -= damage;
        sfxMan.PlayerHurt.Play();
    }
    // API used to revive player.
    public void setMaxHealth(){
        playerCurrentHealth = playerMaxHealth;
    }

    public void AddHealth(int change)
    {
        int newHealth = playerCurrentHealth + change;
        if (newHealth > playerMaxHealth)
        {
            playerCurrentHealth = playerMaxHealth;
        }
        else if (newHealth < 0)
        {
            playerCurrentHealth = 0;
        }
        else
        {
            playerCurrentHealth = newHealth;
        }
    }
    public void ChangeMana(int change)
    {
        int newMana = playerCurrentMana + change;
        if (newMana > playerMaxMana)
        {
            playerCurrentMana = playerMaxMana;
        }
        else if (newMana < 0)
        {
            playerCurrentMana = 0;
        }
        else
        {
            playerCurrentMana = newMana;
        }
    }
    public void setMaxMana()
    {
        playerCurrentMana = playerMaxMana;
    }

    // Used to increase player attack at shop
    public void setPlayerDamage(int amount)
    {
        playerCurrentDamage += amount;
    }
    public void setPlayerDefense()
    {
        playerCurrentDefense += 1;
    }
}
