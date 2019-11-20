using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the player's health
public class PlayerHealthManager : MonoBehaviour {

    public int playerMaxHealth;
    public int playerCurrentHealth;
    public int playerCurrentDamage;

	// Use this for initialization
	void Start () {
        playerCurrentHealth = playerMaxHealth;
        playerCurrentDamage = 5;
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
    }
    // API used to revive player.
    public void setMaxHealth(){
        playerCurrentHealth = playerMaxHealth;
    }
}
