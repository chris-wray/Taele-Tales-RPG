using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to load a new area. A portal.
public class LoadFromMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        // Detects a player collision with this portal.
        Debug.Log("Loading from Menu to Main");
            Application.LoadLevel("main");
     
    }
}
