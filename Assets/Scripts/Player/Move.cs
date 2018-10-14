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
    public Transform idleAnimation;
    public Transform moveAnimation;
    public Transform glideAnimation;
    public Transform jumpAnimation;
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
        jumpAnimation.gameObject.SetActive(false);
        moveAnimation.gameObject.SetActive(false);
        glideAnimation.gameObject.SetActive(false);
        idleAnimation.gameObject.SetActive(true);
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
            }
            jumpAnimation.gameObject.SetActive(false);
            idleAnimation.gameObject.SetActive(true);
            //the jumpcounter is whatever we set jumptime to in the editor.
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            idleAnimation.gameObject.SetActive(false); //These will be replaced by transitions
            moveAnimation.gameObject.SetActive(false);
            start = true;
        }
        if ((Input.GetButton("Jump")) && !stoppedJumping)
        {
            idleAnimation.gameObject.SetActive(false);
            moveAnimation.gameObject.SetActive(false);
            mid = true;
        }
        if(Input.GetButtonUp("Jump"))
        {
            idleAnimation.gameObject.SetActive(false);
            moveAnimation.gameObject.SetActive(false);
            end = true;
        }
        if (Input.GetButton("Glide"))
        {
            jumpAnimation.gameObject.SetActive(false);
            moveAnimation.gameObject.SetActive(false);
            idleAnimation.gameObject.SetActive(false);
            glide = true;
        }
        if (Input.GetButton("Glide") == false)
        {
            glideAnimation.gameObject.SetActive(false);
            if (!(grounded))
            {
                jumpAnimation.gameObject.SetActive(true);
            }
        }
        if (Input.GetAxis("MovementX") != 0)
        {
            idleAnimation.gameObject.SetActive(false);
            move = true;
        }
        if (!(grounded))
        {
            moveAnimation.gameObject.SetActive(false);
        }

    }

    public void FixedUpdate()
    {
        if(move)
        {
            if(glide == false || grounded == false)
            {
                glideAnimation.gameObject.SetActive(false);
                float moveX = Input.GetAxis("MovementX");
                Vector3 movingVector = new Vector3(moveX * xSpeed, 0.0f, 0.0f);
                if (grounded)
                {
                    moveAnimation.gameObject.SetActive(true);
                }

                transform.Translate(movingVector, Space.World);
                move = false;
            }
            
        }
        if(start)
        {
            //jump!
            rb.velocity = new Vector3(0.0f, jumpForce, 0.0f);
            jumpAnimation.gameObject.SetActive(true);
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
            moveAnimation.gameObject.SetActive(false);
            glideAnimation.gameObject.SetActive(true);
            if (Input.GetAxis("MovementX") > 0.0f)
            {
                glideAnimation.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }
            if (Input.GetAxis("MovementX") < 0.0f)
            {
                glideAnimation.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            }
            rb.velocity = new Vector3(0.0f, glideForce, 0.0f);
            glide = false;          
        }
    }
}