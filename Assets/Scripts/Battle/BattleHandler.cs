using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public GameObject dBox;
    public Text dText;
    GameObject player;
    GameObject enemy;
    public int state; //0 = start, 1 = player turn, 
    //2 = player animations, 3 = enemy turn, 
    //4 = enemy animations, 5 = player win, 6 = enemy win;
    public int playerAction; //0 = attack, 1 = strong attack,
    //2 = defend, 3 = flee, 4 = none (continue waiting for input)
    public int prevAction;
    public int mobDamage;
    public int playerDamage;
    System.Random rand;
    public bool mobAction;

    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        state = 0;
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerControl>().StopForBattle();
        player.GetComponent<PlayerControl>().enabled = false;
        playerDamage = player.GetComponent<PlayerHealthManager>().playerCurrentDamage;

        playerAction = 4;

        enemy = GameObject.FindWithTag("Enemy");
        mobDamage = enemy.GetComponent<EnemyHealthManager>().mobDamage;
        enemy.GetComponent<SlimeController>().Stop();
        enemy.GetComponent<SlimeController>().enabled = false;
        dText.text = "Enemy Health: " + enemy.GetComponent<EnemyHealthManager>().mobCurrentHealth;
        mobAction = false;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        //Debug.Log(playerAction);
        switch(state) //switch statement to check the "turn" the game is on
        {
            case 0: //if battle just started
                state++;
                break;
            case 1: //if it is the player's turn
                prevAction = playerAction;
                switch (playerAction)
                {
                    case 0: //if player chooses to attack
                        enemy.GetComponent<EnemyHealthManager>().HurtEnemy(playerDamage);
                        Debug.Log(enemy.GetComponent<EnemyHealthManager>().mobCurrentHealth);
                        if (enemy.GetComponent<EnemyHealthManager>().mobCurrentHealth <= 0)
                        {
                            state = 5;
                        }
                        else
                        {
                            state++;
                        }
                        break;
                    case 1: //if player chooses to perform a strong attack
                        enemy.GetComponent<EnemyHealthManager>().HurtEnemy(playerDamage*2);
                        if (enemy.GetComponent<EnemyHealthManager>().mobCurrentHealth <= 0)
                        {
                            state = 5;
                        }
                        else
                        {
                            state++;
                        }
                        break;
                    case 2: //if player chooses to defend
                        state++;
                        break;
                    case 3: //if player chooses to flee
                        state = 7;
                        break;
                    case 4: //if player has not chosen yet
                        break;
                }
                playerAction = 4;
                break;
            case 2: //player animations
                state++;
                break;
            case 3:
                //Debug.Log("Here");
                if(mobAction)//if the enemy prepared a strong attack last turn, it does a strong attack that deals triple damage.
                {
                    mobAction = false;
                    switch (prevAction)
                    {

                        case 0:
                            dText.text = "The enemy attacks you with a strong attack! It deals " + mobDamage * 3 + " damage to you.";
                            player.GetComponent<PlayerHealthManager>().HurtPlayer(mobDamage*3);
                            break;
                        case 1:
                            dText.text = "The enemy attacks you with a strong attack! It deals " + mobDamage * 6 + " damage to you. Ouch!";
                            player.GetComponent<PlayerHealthManager>().HurtPlayer(mobDamage * 6);
                            break;
                        case 2:
                            dText.text = "The enemy attacks you! It deals " + mobDamage + " damage to you, through your defense.";
                            player.GetComponent<PlayerHealthManager>().HurtPlayer(mobDamage);
                            break;
                    }
                }
                else
                {
                    switch (rand.Next(0, 2))
                    {
                        case 0:
                            switch (prevAction)
                            {
                                case 0:
                                    dText.text = "The enemy attacks you! It deals " + mobDamage + " damage to you.";
                                    player.GetComponent<PlayerHealthManager>().HurtPlayer(mobDamage);
                                    break;
                                case 1:
                                    dText.text = "The enemy attacks you! It deals " + mobDamage * 2 + " damage to you. Ouch!";
                                    player.GetComponent<PlayerHealthManager>().HurtPlayer(mobDamage * 2);
                                    break;
                                case 2:
                                    dText.text = "The enemy attacks you! It deals " + mobDamage / 2 + " damage to you, through your defense.";
                                    player.GetComponent<PlayerHealthManager>().HurtPlayer(mobDamage / 2);
                                    break;
                            }
                            break;
                        case 1:
                            dText.text = "The enemy prepares a strong attack!";
                            mobAction = true;
                            break;
                    }
                }
                state++;
                break;
            case 4:
                state = 0;
                break;
            case 5:
                dText.text = "You won!";
                player.GetComponent<PlayerControl>().enabled = true;
                Application.LoadLevel("main");
                break;
            case 6:
                dText.text = "You lost.";
                Application.LoadLevel("main");
                break;
            case 7:
                dText.text = "You ran from battle.";
                Application.LoadLevel("main");
                break;
        }
    }
}
