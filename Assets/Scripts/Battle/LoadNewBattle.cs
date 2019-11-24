using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            thePlayer.prevScene = SceneManager.GetActiveScene().name;
            thePlayer.prevPos = thePlayer.transform.position;
            thePlayer.prevDirection = thePlayer.lastMove;

            diffMan.difficultyPanel.SetActive(false);
            Application.LoadLevel(levelToLoad);
        }
    }
}
