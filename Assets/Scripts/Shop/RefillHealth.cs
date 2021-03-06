using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefillHealth : MonoBehaviour
{

    public AudioClip FailSound;
    public AudioClip SuccessSound;
    public AudioSource audioSource;

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
        if (money.currentGold >= 3)
        {
            Debug.Log("refill success");
            sfxMan.PurchaseAccepted.Play();
            money.AddMoney(-3);
            player.GetComponent<PlayerHealthManager>().setMaxHealth();
        }
        else
        {
            Debug.Log("refill failed");
            sfxMan.PurchaseDenied.Play();
        }
        audioSource.Play();
    }
}
