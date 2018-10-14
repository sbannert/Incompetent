using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    /*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
     * and a counter to track how long you have been jumping*/
    public float xSpeed;
    public float jumpForce;
    public float glideForce;
    public float jumpTime;
    public float jumpTimeCounter;
    //animations
    public GameObject idleAnimation;
    public GameObject moveRightAnimation;
    public GameObject jumpAnimation;
    public GameObject moveLeftAnimation;
    public GameObject glideLeftAnimation;
    public GameObject glideRightAnimation;
    /*this bool is to tell us whether you are on the ground or not
     * the layermask lets you select a layer to be ground; you will need to create a layer named ground(or whatever you like) and assign your
     * ground objects to this layer.
     * The stoppedJumping bool lets us track when the player stops jumping.*/
    [HideInInspector]
    public bool grounded, move, start, mid, end, glide, stoppedJumping;
    public LayerMask whatIsGround;
    /*the public transform is how you will detect whether we are touching the ground.
     * Add an empty game object as a child of your player and position it at your feet, where you touch the ground.
     * the float groundCheckRadius allows you to set a radius for the groundCheck, to adjust the way you interact with the ground*/
    public Transform groundCheck;
    
    //You will need a rigidbody to apply forces for jumping, in this case I am using Rigidbody 2D because we are trying to emulate Mario :)
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //sets the jumpCounter to whatever we set our jumptime to in the editor
        xSpeed = 0.1f;
        jumpForce = 5f;
        glideForce = -0.5f;
        jumpTime = 0.4f;
        jumpTimeCounter = jumpTime;
        start = false;
        mid = false;
        end = false;
        glide = false;
        move = false;
        jumpAnimation.SetActive(false);
        moveRightAnimation.SetActive(false);
        moveLeftAnimation.SetActive(false);
        glideRightAnimation.SetActive(false);
        glideLeftAnimation.SetActive(false);
        idleAnimation.SetActive(false);
        Debug.Log("loaded");
    }

    void Update()
    {
        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        grounded = Physics.CheckBox(groundCheck.position, new Vector3(0.47f, 0.1f, 0.9f), new Quaternion(0f, 0f, 0f, 0f), whatIsGround);


        //if we are grounded...
        if (grounded)
        {
            if (!(Input.GetButton("Jump") || Input.GetButton("Glide") || Input.GetAxis("MovementX") != 0))
            {
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                jumpAnimation.SetActive(false);
                moveRightAnimation.SetActive(false);
                moveLeftAnimation.SetActive(false);
                glideRightAnimation.SetActive(false);
                glideLeftAnimation.SetActive(false);
            }
            jumpAnimation.SetActive(false);
            idleAnimation.SetActive(true);
            //the jumpcounter is whatever we set jumptime to in the editor.
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            idleAnimation.SetActive(false); //These will be replaced by transitions
            moveRightAnimation.SetActive(false);
            moveLeftAnimation.SetActive(false);
            start = true;
        }
        if ((Input.GetButton("Jump")) && !stoppedJumping)
        {
            idleAnimation.SetActive(false);
            moveLeftAnimation.SetActive(false);
            moveRightAnimation.SetActive(false);
            mid = true;
        }
        if(Input.GetButtonUp("Jump"))
        {
            idleAnimation.SetActive(false);
            moveRightAnimation.SetActive(false);
            moveLeftAnimation.SetActive(false);
            end = true;
        }
        if (Input.GetButton("Glide"))
        {
            jumpAnimation.SetActive(false);
            moveRightAnimation.SetActive(false);
            moveLeftAnimation.SetActive(false);
            idleAnimation.SetActive(false);
            glide = true;
        }
        if (Input.GetButton("Glide") == false)
        {
            glideRightAnimation.SetActive(false);
            glideLeftAnimation.SetActive(false);
            if (!(grounded))
            {
                jumpAnimation.SetActive(true);
            }
        }
        if (Input.GetAxis("MovementX") > 0)
        {
            idleAnimation.SetActive(false);
            move = true;
        }
        if (Input.GetAxis("MovementX") < 0)
        {
            idleAnimation.SetActive(false);
            move = true;
        }
        if (!(grounded))
        {
            moveRightAnimation.SetActive(false);
            moveLeftAnimation.SetActive(false);
        }

    }

    public void FixedUpdate()
    {
        if(move)
        {
            if(glide == false || grounded == false)
            {
                glideRightAnimation.SetActive(false);
                glideLeftAnimation.SetActive(false);
                float moveX = Input.GetAxis("MovementX");
                Vector3 movingVector = new Vector3(moveX * xSpeed, 0.0f, 0.0f);
                if (grounded)
                {
                    if (Input.GetAxis("MovementX") > 0.0f)
                    {
                        moveRightAnimation.SetActive(true);
                    }
                    if (Input.GetAxis("MovementX") < 0.0f)
                    {
                        moveLeftAnimation.SetActive(true);
                    }
                }

                transform.Translate(movingVector, Space.World);
                move = false;
            }
            
        }
        if(start)
        {
            //jump!
            rb.velocity = new Vector3(0.0f, jumpForce, 0.0f);
            jumpAnimation.SetActive(true);
            stoppedJumping = false;
            start = false;
        }
        //if you keep holding down the jump button...
        if (mid)
        {
            //and your counter hasn't reached zero...
            if (jumpTimeCounter > 0)
            {
                //keep jumping!
                rb.velocity = new Vector3(0.0f, jumpForce, 0.0f);
                jumpTimeCounter -= Time.deltaTime;
                mid = false;
            }
        }
        //if you stop holding down the jump button...
        if (end)
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            jumpTimeCounter = 0;
            stoppedJumping = true;
            end = false;
            mid = false;
        }
        if (glide)
        {       
            moveRightAnimation.SetActive(false);
            moveLeftAnimation.SetActive(false);

            if (Input.GetAxis("MovementX") > 0.0f)
            {
                glideLeftAnimation.SetActive(true);                
            }
            else if (Input.GetAxis("MovementX") < 0.0f)
            {
                glideRightAnimation.SetActive(true);               
            }
            else
            {
                glideRightAnimation.SetActive(true);
            }
            rb.velocity = new Vector3(0.0f, glideForce, 0.0f);
            glide = false;          
        }
    }
}