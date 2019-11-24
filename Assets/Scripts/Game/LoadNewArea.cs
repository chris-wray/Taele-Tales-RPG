using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to load a new area. A portal.
public class LoadNewArea : MonoBehaviour {

    public string levelToLoad;
    public string exitPoint;

    public PlayerControl thePlayer;
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
        // Detects a player collision with this portal.
        if(other.gameObject.name == "Player"){
            Debug.Log("Entering new area: " + levelToLoad);
            thePlayer.startPointName = exitPoint;
            Application.LoadLevel(levelToLoad);
        }
    }
}
