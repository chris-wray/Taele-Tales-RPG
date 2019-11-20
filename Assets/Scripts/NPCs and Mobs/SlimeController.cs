using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour {

    public float moveSpeed;

    private Rigidbody2D myRigidbody;

    private bool moving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    public float waitToReload;
    private bool reloading;
    private GameObject thePlayer;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();

        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;
	}
	
	// Update is called once per frame
	void Update () {
        if (moving) {
            myRigidbody.velocity = moveDirection;
            timeToMoveCounter -= Time.deltaTime;

            if(timeToMoveCounter < 0f){
                moving = false;
                timeBetweenMoveCounter = Random.Range(0f, 3f);
            }
        }
        else{
            myRigidbody.velocity = Vector2.zero;
            timeBetweenMoveCounter -= Time.deltaTime;

            if(timeBetweenMoveCounter < 0f){
                moving = true;
                timeToMoveCounter = Random.Range(0f, 3f);

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed,Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
        }
        if (reloading){
            waitToReload -= Time.deltaTime;
            if(waitToReload < 0){
                Application.LoadLevel(Application.loadedLevel);
                thePlayer.SetActive(true);
                reloading = false;
            }
        }
	}
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name == "Player"){
            //Destroy(other.gameObject);
            /*other.gameObject.SetActive(false);
            reloading = true;
            thePlayer = other.gameObject;*/
        }
    }

    public void Stop()
    {
        moving = false;
    }
}
