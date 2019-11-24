using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls slime enemy objects
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

	// Sets up and initializes the slimes
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();

        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;
        thePlayer = GameObject.FindWithTag("Player");

        if (Vector3.Distance(thePlayer.transform.position, gameObject.transform.position) < 5)
        {
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	// Moves a slime around for a fixed amount of time if it can.
        if (moving) {
            myRigidbody.velocity = moveDirection;
            timeToMoveCounter -= Time.deltaTime;

            if(timeToMoveCounter < 0f){
                moving = false;
                timeBetweenMoveCounter = Random.Range(0f, 3f);
            }
        }
        // Waits on the ability to move for a randomized amount of time to a random location.
        else{
            myRigidbody.velocity = Vector2.zero;
            timeBetweenMoveCounter -= Time.deltaTime;

            if(timeBetweenMoveCounter < 0f){
                moving = true;
                timeToMoveCounter = Random.Range(0f, 3f);

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed,Random.Range(-1f, 1f) * moveSpeed, 0f);
            }
        }
        // Waits to respawn the slime for waitToReload time.
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
        // Detects slime collision with players.
        if(other.gameObject.name == "Player"){
            //Destroy(other.gameObject);
            /*other.gameObject.SetActive(false);
            reloading = true;
            thePlayer = other.gameObject;*/
        }
    }
    // Stops slime movement.
    public void Stop()
    {
        moving = false;
    }
}
