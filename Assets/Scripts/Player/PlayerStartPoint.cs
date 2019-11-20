using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles setting the player up at the start location at the beginning of the game.
public class PlayerStartPoint : MonoBehaviour {

    private PlayerControl thePlayer;
    private CameraController theCamera;

    public Vector2 startDirection;

	// Use this for initialization - Set the player to the start location.
	void Start () {
        thePlayer = FindObjectOfType<PlayerControl>();
        thePlayer.transform.position = transform.position;
        thePlayer.lastMove = startDirection;

        Debug.Log("Player Start Point: " + startDirection);

        theCamera = FindObjectOfType<CameraController>();
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
