using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public int mobMaxHealth;
    public int mobCurrentHealth;

    // Use this for initialization
    void Start()
    {
        mobCurrentHealth = mobMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (mobCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void HurtEnemy(int damage)
    {
        mobCurrentHealth -= damage;
    }
    public void setMaxHealth()
    {
        mobCurrentHealth = mobMaxHealth;
    }
}
