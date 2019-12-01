using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to load a new area. A portal.
public class LoadFromMenu : MonoBehaviour
{

    public AudioClip ButtonSound;
    public AudioSource audioSource;


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
        audioSource.clip = ButtonSound;
        audioSource.Play();
        // Detects a player collision with this portal.
        Debug.Log("Loading from Menu to Main");
            Application.LoadLevel("main");
     
    }
}
