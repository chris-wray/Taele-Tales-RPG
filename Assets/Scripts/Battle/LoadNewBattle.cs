using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewBattle : MonoBehaviour {

    public string levelToLoad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "Player"){
            DontDestroyOnLoad(gameObject); //avoid destroying the encountered monster on load; player is already preserved
            Application.LoadLevel(levelToLoad);
        }
    }
}
