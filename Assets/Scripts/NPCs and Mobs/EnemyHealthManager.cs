using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages enemy health and combat interaction.
public class EnemyHealthManager : MonoBehaviour {

    public int mobMaxHealth;
    public int mobCurrentHealth;
    public int mobDamage;
    public GameObject coinDrop;
    public int coinValue;

    DifficultyManager diffMan;

    // Use this for initialization
    void Start()
    {
        mobCurrentHealth = mobMaxHealth;
        mobDamage = 5;

        // If game is on peaceful, kills the enemy
        diffMan = FindObjectOfType<DifficultyManager>();
    }

    // Update is called once per frame
    void Update()
    {
	// Check if this monster has died.
        if (mobCurrentHealth <= 0 || diffMan.difficultyLevel == "Peaceful")
        {
            gameObject.SetActive(false);
            if (diffMan.difficultyLevel != "Peaceful")
            {
                Instantiate(coinDrop, transform.position, Quaternion.identity);
            }
        }
    }
    // Used by weapons to damage the enemy.
    public void HurtEnemy(int damage)
    {
        mobCurrentHealth -= damage;
    }
    // Used to revive an object rather than making a new one.
    public void setMaxHealth()
    {
        mobCurrentHealth = mobMaxHealth;
    }
}
