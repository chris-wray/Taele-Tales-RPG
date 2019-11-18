using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if(playerCurrentHealth <= 0){
            gameObject.SetActive(false);
        }
	}
    public void HurtPlayer(int damage){
        playerCurrentHealth -= damage;
    }
    public void setMaxHealth(){
        playerCurrentHealth = playerMaxHealth;
    }
}
