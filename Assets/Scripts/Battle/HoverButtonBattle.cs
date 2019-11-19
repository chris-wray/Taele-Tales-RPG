using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverButtonBattle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int value; //0 = basic attack, 1 = strong attack,
    //2 = defend, 3 = flee

    public bool isActive; //keeps track of whether the button is held
    public BattleHandler bh; //the active BattleHandler
    public float time; //time the button has been held for
    public Text dText; //the text box of the button

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        time = 0.0f;
        switch(value)
        {
            case 0:
                dText.text = "Attack\n Deal " + bh.playerDamage + " damage to the enemy.";
                break;
            case 1:
                dText.text = "Strong Attack\n Deal " + bh.playerDamage*2 + " damage to the enemy, but take double damage on their next turn.";
                break;
            case 2:
                dText.text = "Defend\n Take 1/2 damage from regular attacks, and 1/3rd damage from strong attacks.";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((isActive) && (bh.state==1)) //if button is held and it's the player's turn
        {
            time += Time.deltaTime; //subtract the time since the last frame from the time limit
            //Debug.Log(time);
            if (time >= 2.0f) //if 2 seconds have passed, "click" the button, selecting that combat option
            {
                time = 0.0f;
                OnClick();
                isActive = false;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isActive = true;
        //Debug.Log("Button held");
        //dText.text = "Hovering over button for " + time + "seconds";
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isActive = false;
        time = 0.0f;
    }
    public void OnClick()
    {
        bh.playerAction = value;
    }
}
