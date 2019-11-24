using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DifficultyHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isActive; //keeps track of whether the button is held
    public DifficultyManager diffMan; //the active DifficultyManager
    public float time; //time the button has been held for
    public Text dText; //the text box of the button

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
            //Debug.Log(time);
            if (time >= 2.0f) //if 2 seconds have passed, "click" the button
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
        Debug.Log("Clicked");
        diffMan.changeDifficulty(dText.text);
    }
}
