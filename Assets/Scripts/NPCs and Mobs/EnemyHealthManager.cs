using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public int mobMaxHealth;
    public int mobCurrentHealth;
    public int mobDamage;
    public GameObject coinDrop;

    // Use this for initialization
    void Start()
    {
        mobCurrentHealth = mobMaxHealth;
        mobDamage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (mobCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(coinDrop, transform.position, Quaternion.identity);
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
