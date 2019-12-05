using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// General purpose script that invokes a button press when hovered for long enough
public class HoverButtonClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isActive; //keeps track of whether the button is held
    float time; //time the button has been held for
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) //if button is held
        {
            time += Time.deltaTime; //subtract the time since the last frame from the time limit
            if (time >= PlayerControl.clickSpeed) //if n seconds have passed, "click" the button
            {
                time = 0.0f;
                button.onClick.Invoke();
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.gameObject.activeSelf) //Only activate if button is currently active
        {
            isActive = true;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isActive = false;
        time = 0.0f;
    }
}
