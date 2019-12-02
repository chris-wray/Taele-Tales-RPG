using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{

    public Slider healthBar;
    public Text HPText;
    public EnemyHealthManager mobHealth;

    public static bool UIExists;
    // Start is called before the first frame update
    void Start()
    {
        if (!UIExists)
        {
            mobHealth = GameObject.FindWithTag("Enemy").GetComponent<EnemyHealthManager>();
            UIExists = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = mobHealth.mobMaxHealth;
        healthBar.value = mobHealth.mobCurrentHealth;
        HPText.text = "Enemy HP: " + healthBar.value + "/" + healthBar.maxValue;
    }
}
