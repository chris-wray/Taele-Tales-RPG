using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillMana : MonoBehaviour
{

    private SFXManager sfxMan;

    MoneyManager money;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        money = player.GetComponent<MoneyManager>();
        sfxMan = FindObjectOfType<SFXManager>();
    }

    public void Refill()
    {
        if (money.currentGold >= 5)
        {
            sfxMan.PurchaseAccepted.Play();
            money.AddMoney(-5);
            player.GetComponent<PlayerHealthManager>().setMaxMana();
        }
        else
        {
            sfxMan.PurchaseDenied.Play();
        }
    }
}
