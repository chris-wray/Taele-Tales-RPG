using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles player controls such as moving around, and interaction with in-game objects.
public class PlayerControl : MonoBehaviour {

    public static float moveSpeed = 5;
    public static float clickSpeed = 1;

    private Animator anim;
    private Rigidbody2D myRigidBody;

    private bool playerMoving = false;
    public Vector2 lastMove;

    private static bool playerExists;

    //Note, below should be in the individual weapon's file.
    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    // Add support for mouse-over controls
    public float buttonHorizontal;
    public float buttonVertical;

    // Name of the starting point on the new scene
    public string startPointName;

    // Stores position and scene prior to a battle
    public string prevScene;
    public Vector3 prevPos;
    public Vector2 prevDirection;
    
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

        buttonHorizontal = 0f;
        buttonVertical = 0f;
        if (!playerExists){
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else{
            Destroy(gameObject);
        }
	}

    public void StopForBattle()
    {
        playerMoving = false;
        myRigidBody.velocity = Vector2.zero;
        anim.SetFloat("MoveX", 0f);
        anim.SetFloat("MoveY", 0f);
        anim.SetFloat("LastMoveY", 0f);
        anim.SetFloat("LastMoveX", 1f);
        anim.SetBool("PlayerMoving", playerMoving);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Last Move: " + lastMove);
        playerMoving = false;
        float axisHorizontal = Input.GetAxisRaw("Horizontal");
        if (buttonHorizontal != 0) {
            axisHorizontal = buttonHorizontal;
        }

        float axisVertical = Input.GetAxisRaw("Vertical");
        if (buttonVertical != 0) {
            axisVertical = buttonVertical;
        }

        if (!attacking){
            if (axisHorizontal > 0.5f || axisHorizontal < -0.5f)
            {
                //transform.Translate(new Vector3(axisHorizontal * moveSpeed * Time.deltaTime, 0f, 0f));
                myRigidBody.velocity = new Vector2(axisHorizontal * moveSpeed, myRigidBody.velocity.y);
                playerMoving = true;
                lastMove = new Vector2(axisHorizontal, 0f);
            }

            if (axisVertical > 0.5f || axisVertical < -0.5f)
            {
                //transform.Translate(new Vector3(0f, axisVertical * moveSpeed * Time.deltaTime, 0f));
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, axisVertical * moveSpeed);
                playerMoving = true;
                lastMove = new Vector2(0f, axisVertical);
            }

            if (axisHorizontal < 0.5f && axisHorizontal > -0.5f)
            {
                myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
            }
            if (axisVertical < 0.5f && axisVertical > -.5f)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0f);
            }
            if (Input.GetKeyDown("space"))
            {
                attackTimeCounter = attackTime;
                attacking = true;
                myRigidBody.velocity = Vector2.zero;
                anim.SetBool("Attacking", true);
            }
        }
        
        if(attackTimeCounter > 0){
            attackTimeCounter -= Time.deltaTime;
        }
        if(attackTimeCounter <= 0){
            attacking = false;
            anim.SetBool("Attacking", false);
        }

        anim.SetFloat("MoveX", axisHorizontal);
        anim.SetFloat("MoveY", axisVertical);
        anim.SetFloat("LastMoveY", lastMove.y);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetBool("PlayerMoving", playerMoving);
	}

        public static void AdjustMoveSpeed(float newSpeed){
            moveSpeed = newSpeed;
        }
        public static void AdjustClickSpeed(float newSpeed){
            clickSpeed = 1/newSpeed;
        }
}
