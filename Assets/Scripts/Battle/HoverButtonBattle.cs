using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverButtonBattle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SFXManager sfxMan;
    private PlayerHealthManager playerHealth;
    public GameObject SpellMenu;

    public int value; //0 = basic attack, 1 = switch to spell menu,
                      //2 = defend, 3 = flee
                      // 5 = heal spell, 6 = back to action menu
                      // 7 = Increase attack spell, 8 = damage spell

    public bool isActive; //keeps track of whether the button is held
    public BattleHandler bh; //the active BattleHandler
    public float time; //time the button has been held for

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        time = 0.0f;
        sfxMan = FindObjectOfType<SFXManager>();
        playerHealth = FindObjectOfType<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((isActive) && ((bh.state==1)||(value==10))) //if button is held and it's the player's turn, or if this is the end battle button
        {
            time += Time.deltaTime; //subtract the time since the last frame from the time limit
            //Debug.Log(time);
            if (time >= PlayerControl.clickSpeed) //if n seconds have passed, "click" the button, selecting that combat option
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
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isActive = false;
        time = 0.0f;
    }
    public void OnClick()
    {
        switch (value)
        {
            case 1: // Opens spell menu
                SpellMenu.SetActive(true);
                sfxMan.ButtonClick.Play();
                break;
            case 5: // Heal spell
                if (playerHealth.playerCurrentMana >= 5)
                {
                    playerHealth.ChangeMana(-5);
                    bh.playerAction = value;
                    sfxMan.ButtonClick.Play();
                } else
                {
                    sfxMan.PurchaseDenied.Play();
                }
                break;
            case 6: // Closes spell menu
                SpellMenu.SetActive(false);
                sfxMan.ButtonClick.Play();
                break;
            case 7: // Attack spell
                if (playerHealth.playerCurrentMana >= 3)
                {
                    playerHealth.ChangeMana(-3);
                    bh.playerAction = value;
                    sfxMan.ButtonClick.Play();
                }
                else
                {
                    sfxMan.PurchaseDenied.Play();
                }
                break;
            case 8: // Damage spell
                if (playerHealth.playerCurrentMana >= 7)
                {
                    playerHealth.ChangeMana(-7);
                    bh.playerAction = value;
                    sfxMan.ButtonClick.Play();
                }
                else
                {
                    sfxMan.PurchaseDenied.Play();
                }
                break;
            default: // Other action
                bh.playerAction = value;
                sfxMan.ButtonClick.Play();
                break;
        }
    }
}
