using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefillHealth : MonoBehaviour
{
    MoneyManager money;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        money = player.GetComponent<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refill()
    {
        Debug.Log("refill");
        if (money.currentGold >= 5)
        {
            money.AddMoney(-5);
            player.GetComponent<PlayerHealthManager>().setMaxHealth();
        }
    }
}
