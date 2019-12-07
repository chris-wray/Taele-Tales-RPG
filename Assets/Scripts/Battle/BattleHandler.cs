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

    public int playerAction; //0 = attack, 1 = spell menu,
    //2 = defend, 3 = flee, 4 = none (continue waiting for input)
    // 5 = heal spell, 6 = back to action menu
    // 7 = Increase attack spell, 8 = damage spell

    //integers to locally track the monster's and player's stats
    int mobAttack;
    int playerAttack; 
    int playerDefense;

    System.Random rand;//RNG to determine monster actions

    bool preparingAttack; //keeps track of whether the monster is preparing a strong attack
    bool isPlayerDefending;
    int offenseBuffCounter; // Counts how many turns of offense buff are left
    DifficultyManager diffMan;
    int difficultyMultiplier;

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

        playerAttack = player.GetComponent<PlayerHealthManager>().playerCurrentDamage;
        playerDefense = player.GetComponent<PlayerHealthManager>().playerCurrentDefense;

        playerAction = 4;

        //find the enemy object, which should also be preserved from the previous scene
        enemy = GameObject.FindWithTag("Enemy");
        mobAttack = enemy.GetComponent<EnemyHealthManager>().mobDamage;

        preparingAttack = false;
        isPlayerDefending = false;
        offenseBuffCounter = 0;

        diffMan = FindObjectOfType<DifficultyManager>();
        switch (diffMan.difficultyLevel)
        {
            case "Peaceful":
                difficultyMultiplier = 0;
                break;
            case "Hard":
                difficultyMultiplier = 5;
                break;
            default: // Normal or undefined
                difficultyMultiplier = 1;
                break;
        }
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
        if(player.GetComponent<PlayerHealthManager>().playerCurrentHealth <= 0) //if player is dead, set state to 6, where player has lost
        {
            state = 6;
        }
    }

    void ResolvePlayerTurn()
    {
        isPlayerDefending = false;
        int baseDamage = 0;

        switch (playerAction)
        {
            case 0: //if player chooses to attack, deal playerDamage damage to enemy
                baseDamage = 1;
                break;
            case 2: //if player chooses to defend, increment the state
                isPlayerDefending = true;
                state++;
                break;
            case 3: //if player chooses to flee, set state to 7, which is the flee state
                state = 7;
                break;
            case 4: //if player has not chosen an action yet, do nothing
                break;
            case 5: // Heals player 20 HP
                player.GetComponentInChildren<PlayerHealthManager>().AddHealth(20);
                state++;
                break;
            case 7: // Adds attack buff
                offenseBuffCounter += 4;
                state++;
                break;
            case 8: // Strong magic attack
                baseDamage = 5;
                break;
        }

        if (baseDamage > 0)
        {
            // Player Damage formula
            int damageDealt = playerAttack * baseDamage;
            if (offenseBuffCounter > 0)
            {
                damageDealt = (int) (1.25 * damageDealt);
            }

            enemy.GetComponent<EnemyHealthManager>().HurtEnemy(damageDealt);
            Debug.Log(enemy.GetComponent<EnemyHealthManager>().mobCurrentHealth);
            if (enemy.GetComponent<EnemyHealthManager>().mobCurrentHealth <= 0)
            {
                state = 5; //if the monster dies, move to state 5, which is victory, or increment the state to continue the battle
            }
            else
            {
                state++;
            }
        }

        if (offenseBuffCounter > 0)
        {
            offenseBuffCounter--;
        }
    }

    void ResolveMobTurn()
    {
        int baseDamage = 1; // Normal attack strength
        if (preparingAttack)//if the enemy prepared a strong attack last turn, it does a strong attack that deals triple damage, or just normal damage when defended against.
        {
            baseDamage = 3;
        }
        else if ((rand.Next(0, 5) == 0))
        { // The enemy prepares a strong attack 20% of the time
            preparingAttack = true;
            dText.text = "The enemy prepares a strong attack. Defend yourself!";
            state++;
            playerAction = 4;
            return;
        }

        int playerOverallDefense = playerDefense; // Defense with special attributes
        if (isPlayerDefending)
        {
            playerOverallDefense = playerOverallDefense * 2 + 4;
        }

        // Enemy Damage formula
        int damageDealt = (baseDamage * mobAttack - playerOverallDefense) * difficultyMultiplier;


        if (damageDealt < 0)
        {
            damageDealt = 0;
        }

        if (preparingAttack)
        {
            preparingAttack = false;
            dText.text = "The enemy unleashes a strong attack!";
        }
        else
        {
            dText.text = "The enemy attacks you!";
        }

        dText.text += " It deals " + damageDealt + " damage to you.";

        if (damageDealt == 0)
        {
            dText.text += " That felt like nothing!";
        }
        else if (damageDealt > 20)
        {
            dText.text += " Ouch!";
        }

        player.GetComponent<PlayerHealthManager>().HurtPlayer(damageDealt);

        state++;
        playerAction = 4;
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
                dText.text = "You lost. Click the button to close the game.";
                if (playerAction == 10)
                {
                    // TODO replace with a proper end screen
                    #if UNITY_EDITOR
                    // Application.Quit() does not work in the editor so
                    // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                    //Application.LoadLevel(player.GetComponent<PlayerControl>().prevScene); //return to previous scene
                    //diffMan.difficultyPanel.SetActive(true);
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
