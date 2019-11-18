using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverButtonBattle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int value; //0 = basic attack, 1 = strong attack,
    //2 = defend, 3 = flee
    public bool isActive;
    public BattleHandler bh;
    public float time;
    public Text dText;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        time = 0.0f;
        switch(value)
        {
            case 0:
                dText.text = "Attack\n You will deal " + bh.playerDamage + " damage to the enemy.";
                break;
            case 1:
                dText.text = "Strong Attack\n You will deal " + bh.playerDamage*2 + " damage to the enemy and take double.";
                break;
            case 2:
                dText.text = "Defend\n You will only take " + bh.mobDamage/2 + " damage from the enemy.";
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
            if (time >= 2.0f)
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
        switch (value)
        {
            case 0:
                dText.text = "Attack\n You will deal " + bh.playerDamage + " damage to the enemy.";
                break;
            case 1:
                dText.text = "Strong Attack\n You will deal " + bh.playerDamage * 2 + " damage to the enemy and take double.";
                break;
            case 2:
                dText.text = "Defend\n You will only take " + bh.mobDamage / 2 + " damage from the enemy.";
                break;
        }
    }
    public void OnClick()
    {
        bh.playerAction = value;
    }
}
