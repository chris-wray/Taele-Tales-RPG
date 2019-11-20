using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Identifies hover on movement controls.
public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerControl playerControl;
    public float axisVertical; // How much it changes the vertical axis movement on the player
    public float axisHorizontal;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        playerControl.buttonVertical = axisVertical;
        playerControl.buttonHorizontal = axisHorizontal;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        playerControl.buttonVertical = 0f;
        playerControl.buttonHorizontal = 0f;
    }
}
