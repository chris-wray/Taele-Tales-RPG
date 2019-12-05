using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to load a new area. A portal.
public class LoadFromMenu : MonoBehaviour
{

    public AudioClip ButtonSound;
    public AudioSource audioSource;

    public void OnClick()
    {
        //audioSource.clip = ButtonSound;
        //audioSource.Play();
        Debug.Log("Loading from Menu to Main");
        Application.LoadLevel("mainArea");
     
    }
}
