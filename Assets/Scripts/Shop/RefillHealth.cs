using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefillHealth : MonoBehaviour
{

    public AudioClip FailSound;
    public AudioClip SuccessSound;
    public AudioSource audioSource;

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


        if (money.currentGold >= 5)
        {
            Debug.Log("refill success");
            audioSource.clip = SuccessSound;
            money.AddMoney(-5);
            player.GetComponent<PlayerHealthManager>().setMaxHealth();
        }
        else
        {
            Debug.Log("refill failed");
            audioSource.clip = FailSound;
        }
        audioSource.Play();

    }
}
