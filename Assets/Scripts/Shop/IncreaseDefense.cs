using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDefense : MonoBehaviour
{

    public AudioClip FailSound;
    public AudioClip SuccessSound;
    public AudioSource audioSource;

    MoneyManager money;
    public GameObject player;
    PlayerHealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        money = player.GetComponent<MoneyManager>();
        healthManager = player.GetComponent<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDefense()
    {
        // if player has more than 3 gold
        if (money.currentGold >= 3)
        {
            Debug.Log("increase defense");

            audioSource.clip = SuccessSound;
            money.AddMoney(-3);
            // increase playerCurrentDamage by 1; max damage of mob is 5, max damage of big mob is 10
            // if you want to check for that and then not allow player to purchase increase defense anymore
            // if(enemy.GetComponent<EnemyHealthManager>().mobDamage > 1) {}
            healthManager.setPlayerDefense();
        }
        else
        {
            Debug.Log("increase defense failed");
            audioSource.clip = FailSound;
        }
        audioSource.Play();

    }
}
