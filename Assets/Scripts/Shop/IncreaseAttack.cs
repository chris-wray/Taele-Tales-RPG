using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttack : MonoBehaviour
{

    private SFXManager sfxMan;

    MoneyManager money;
    public GameObject player;
    PlayerHealthManager healthManager;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        money = player.GetComponent<MoneyManager>();
        healthManager = player.GetComponent<PlayerHealthManager>();
        sfxMan = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAttack()
    {
        // if player has more than 3 gold
        if (money.currentGold >= 3)
        {
            Debug.Log("increase attack");
            sfxMan.PurchaseAccepted.Play();

            money.AddMoney(-3);
            // increase playerCurrentDamage by 1; max health of mob is 15, max health of big mob is 25
            healthManager.setPlayerDamage(1);
        }
        else
        {
            Debug.Log("increase attack failed");
            sfxMan.PurchaseDenied.Play();
        }
        
    }
}
