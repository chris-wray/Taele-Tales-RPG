using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public GameObject dBox;
    public Text dText; //description box and text box

    public GameObject EndBattleButton;

    GameObject player;
    PlayerControl playerControl;
    GameObject enemy;

    public int state; //0 = start, 1 = player turn, 
    //2 = player animations, 3 = enemy turn, 
    //4 = enemy animations, 5 = player win,  player.GetComponent<PlayerControl>()
    //6 = enemy win, 7 = flee;

    public int playerAction; //0 = attack, 1 = strong attack,
    //2 = defend, 3 = flee, 4 = none (continue waiting for input)

    public int prevAction; //keeps track of player's previous action,
    //because that action affects the monster's actions

    public int mobDamage;
    public int playerDamage; //integers to locally track the monster's and player's damage

    System.Random rand;//RNG to determine monster actions

    public bool preparingAttack; //keeps track of whether the monster is preparing a strong attack

    DifficultyManager diffMan;

    // Start is called before the first frame update
    void Start()
    {
        EndBattleButton.SetActive(false);
        rand = new System.Random();
        state = 0; 

        //find the player object, which should be preserved from the previous scene
        player = GameObject.FindWithTag("Player");
        //make the player stop moving for the battle:
        playerControl = player.GetComponent<PlayerControl>();
        playerControl.StopForBattle();
        playerControl.enabled = false;

        playerDamage = player.GetComponent<PlayerHealthManager>().playerCurrentDamage;

        playerAction = 4;

        //find the enemy object, which should also be preserved from the previous scene
        enemy = GameObject.FindWithTag("Enemy");
        mobDamage = enemy.GetComponent<EnemyHealthManager>().mobDamage;
        //enemy.GetComponent<SlimeController>().Stop();
        //enemy.GetComponent<SlimeController>().enabled = false;
        //dText.text = "Enemy Health: " + enemy.GetComponent<EnemyHealthManager>().mobCurrentHealth;
        preparingAttack = false;

        diffMan = FindObjectOfType<DifficultyManager>();
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
                ResolvePlayerTurn();
                break;
            case 2: //player animations
                state++;
                break;
            case 3: //monster turn
                //Debug.Log("Here");
                ResolveMobTurn();
                break;
            case 4: //monster animations, then moving back to start of battle
                state = 0;
                break;
            default: //if the state is >4, the battle is over
                ResolveEndOfBattle();
                break;
        }
    }

    void ResolvePlayerTurn()
    {
        prevAction = playerAction; //set prevAction to playerAction so that monster attacks can respond to the player's previous actions
        switch (playerAction)
        {
            case 0: //if player chooses to attack, deal playerDamage damage to enemy
                enemy.GetComponent<EnemyHealthManager>().HurtEnemy(playerDamage);
                Debug.Log(enemy.GetComponent<EnemyHealthManager>().mobCurrentHealth);
                if (enemy.GetComponent<EnemyHealthManager>().mobCurrentHealth <= 0)
                {
                    state = 5; //if the monster dies, move to state 5, which is victory, or increment the state to continue the battle
                }
                else
                {
                    state++;
                }
                break;
            case 1: //if player chooses to perform a strong attack, deal playerDamage*2 damage to enemy
                enemy.GetComponent<EnemyHealthManager>().HurtEnemy(playerDamage * 2);
                if (enemy.GetComponent<EnemyHealthManager>().mobCurrentHealth <= 0)
                {
                    state = 5;
                }
                else
                {
                    state++;
                }
                break;
            case 2: //if player chooses to defend, increment the state
                state++;
                break;
            case 3: //if player chooses to flee, set state to 7, which is the flee state
                state = 7;
                break;
            case 4: //if player has not chosen an action yet, do nothing
                break;
        }
        playerAction = 4;
    }

    void ResolveMobTurn()
    {
        if (preparingAttack)//if the enemy prepared a strong attack last turn, it does a strong attack that deals triple damage, or just normal damage when defended against.
        {
            preparingAttack = false;
            switch (prevAction) //depending on the player's previous action, the strong attack does varying amounts of damage
            {

                case 0:
                    dText.text = "The enemy attacks you with a strong attack! It deals " + mobDamage * 3 + " damage to you.";
                    player.GetComponent<PlayerHealthManager>().HurtPlayer(mobDamage * 3);
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
        else //if the enemy isn't performing a strong attack, it will either perform a normal attack or prepare a strong attack
        {
            switch (rand.Next(0, 2)) //randomly determine the move used
            {
                case 0:
                    switch (prevAction) //depending on the player's previous action, a normal attack does varying damage.
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
                    dText.text = "The enemy prepares a strong attack! Defend yourself!";
                    preparingAttack = true;
                    break;
            }
        }
        state++;
    }
    
    // Reloads the scene prior to the battle
    void ReloadOverworld()
    {
       playerControl.enabled = true; //allow the player to move
        Application.LoadLevel(player.GetComponent<PlayerControl>().prevScene); //return to previous scene
        diffMan.difficultyPanel.SetActive(true);

        playerControl.transform.position = playerControl.prevPos;
        playerControl.lastMove = playerControl.prevDirection;

        var theCamera = FindObjectOfType<CameraController>();
        theCamera.transform.position = new Vector3(playerControl.transform.position.x, playerControl.transform.position.y, theCamera.transform.position.z);
    }

    void ResolveEndOfBattle()
    {
        EndBattleButton.SetActive(true); //activates the end of battle button
       playerControl.startPointName = "Battle Out";
        switch (state)
        {
            case 5: //if the player wins
                dText.text = "You won! You got " + enemy.GetComponent<EnemyHealthManager>().coinValue + " gold. Click the button to continue.";
                if (playerAction == 10)
                {
                    player.GetComponent<MoneyManager>().AddMoney(enemy.GetComponent<EnemyHealthManager>().coinValue); //add the monster's value to the player's money
                    ReloadOverworld();
                }
                break;
            case 6: //if the player loses
                dText.text = "You lost. Click the button to continue.";
                if (playerAction == 10)
                {
                    // TODO replace with a proper end screen
                    Application.LoadLevel(player.GetComponent<PlayerControl>().prevScene); //return to previous scene
                    diffMan.difficultyPanel.SetActive(true);
                }
                break;
            case 7: //if the player flees
                dText.text = "You ran from battle. Click the button to continue.";
                if (playerAction == 10)
                {
                    Destroy(enemy);
                    ReloadOverworld();
                }
                break;
            default: //in case the state isn't victory, defeat, or fleeing, and the code got here, reset state to 0 and return an error message.
                //this should never happen.
                Debug.Log("Error: invalid turn state, reached end of battle without a valid end state");
                state = 0;
                break;
        }
    }
}
