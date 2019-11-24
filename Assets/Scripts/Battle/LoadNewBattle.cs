﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewBattle : MonoBehaviour {

    public string levelToLoad;

    public PlayerControl thePlayer;
    public string exitPoint;

    DifficultyManager diffMan;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerControl>();
        diffMan = FindObjectOfType<DifficultyManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.name == "Player"){
            DontDestroyOnLoad(gameObject); //avoid destroying the encountered monster on load; player is already preserved
            thePlayer.startPointName = exitPoint;
            diffMan.difficultyPanel.SetActive(false);
            Application.LoadLevel(levelToLoad);
        }
    }
}
