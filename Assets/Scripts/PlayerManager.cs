using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public class PlayerManager : MonoBehaviour {



    public float speedX = 5;
    public float jumpSpeedY = 300;
    
    float playerSpeed;
    float Horizontal;

    bool isFacingRight,isOnTheGround, canDoubleJump, isFireing;
    
    Animator playerAnimator;
    Rigidbody2D playerRigidBody;
    GameObject GroundedTrigger;

    bool isRobot = true;
    public bool isPlayerDeath = false;

    int animationState = 0;

    //shooting 
    Transform ProjectileFirePos;
    public GameObject leftBullet, rightBullet;
    public  float fireRate = 0.5f;
    private float timeToNextFire = 0f;

    // Use this for initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        ProjectileFirePos = transform.FindChild("firePos");
        GroundedTrigger = GameObject.Find("GroundTrigger");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlayerDeath)
        {
            isDeath();
        }
        else
        {
            Flip();
            PlayerControls();
            MovePlayer();
        }

        SetAnimationState();

        
    }

    private void SetAnimationState()
    {
        Debug.Log("Animation State:"+animationState);
        if (animationState == 0) // Idle
        {
            playerAnimator.SetInteger("State", 0);
        }
        else if (animationState == 1) //running
        {
            playerAnimator.SetInteger("State", 1);
        }
        else if (animationState == 2) //jumping
        {
            playerAnimator.SetInteger("State",2);
        }
        else if (animationState == 3) //shooting
        {
            playerAnimator.SetInteger("State", 3);
        }
        else if (animationState == 4) // runnung and shooting
        {
            playerAnimator.SetInteger("State", 4);
        }
        else if (animationState == 5) // slide
        {
            playerAnimator.SetInteger("State", 5);
        }
        else if (animationState == 6) // jump and shoot
        {
            playerAnimator.SetInteger("State", 6);
        }
        else if (animationState == 10) // death
        {
            playerAnimator.SetInteger("State", 10);
        }
    }

    private void PlayerControls()
    {
        Horizontal = Input.GetAxis("Horizontal");
        // Jump  Controls
        if (Input.GetButtonDown("Jump") && isOnTheGround)
        {
            PlayerJump();
        }
        // double jump Controls
        if (Input.GetButtonDown("Jump") && canDoubleJump)
        {
            PlayerJump();
        }

        // Fire Controls
        if (Input.GetButtonDown("Fire"))
        {
            Fire();
        }

        // Change Charecter Controls
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwichCharecter();

        }
    }

    private void isDeath()
    {
        //gameObject.transform.position = Vector2.zero;
        //playerAnimator.SetInteger("State", 10);
        animationState = 10;
    }

    // if player colide with something
    void OnCollisionEnter2D(Collision2D colision)
    {
        // if player is on top of and object or ground, we change the animation state and enable jumping
        if (GroundedTrigger.GetComponent<IsColliding>().IsObjectTriggerCollideing)
        {
            //playerAnimator.SetInteger("State", 0);
            //animationState = 0;
            isOnTheGround = true;            
            canDoubleJump = false;
        }
    }

    private void SwichCharecter()
    {
        if (isRobot)
        {
            isRobot = false;
            playerAnimator.runtimeAnimatorController = Resources.Load("Animations/NinjaBoy/NinjaBoy") as RuntimeAnimatorController;
        }
        else
        {
            isRobot = true;
            playerAnimator.runtimeAnimatorController = Resources.Load("Animations/Robot/Robot") as RuntimeAnimatorController;
        }
    }

    void MovePlayer()
    {
        playerSpeed = Horizontal * speedX;
        // if player is moveing left or right without jumping we set animation to Running
        if (isOnTheGround)
        {
            animationState = 0;
            if (playerSpeed != 0) // running = 0
            {
                animationState = 1;
                if (isFireing)
                {
                    animationState = 4; // running and shooting  = 4
                    isFireing = false;
                }
            }
            else if (playerSpeed == 0) // not moveing aka idlee = 0
            {
                animationState = 0;
                if (isFireing)
                {
                    animationState = 3; // shooting from idle = 3
                    isFireing = false;
                }
            }
        }
        else
        {
            animationState = 2;
            if (isFireing)
            {
                animationState = 6;
                isFireing = false;
            }
            
        }
        
          


        playerRigidBody.velocity = new Vector2(playerSpeed, playerRigidBody.velocity.y);
    }

    // fliping the player if needed
    void Flip()
    {
        if (playerSpeed > 0 && !isFacingRight || playerSpeed < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector2 tempVector = transform.localScale;
            tempVector.x *= -1;
            transform.localScale = tempVector;
        }
    }  

    void PlayerJump()
    {
        // single jump
        if (isOnTheGround)
        {
            isOnTheGround = false;            
            playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x, jumpSpeedY));
            //playerAnimator.SetInteger("State", 2);
            //animationState = 2;
            Invoke("EnablePlayerDoubleJump", 0.01f);
        }

        // double jump
        if (canDoubleJump)
        {
            canDoubleJump = false;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0);
            playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x, jumpSpeedY));
            //playerAnimator.SetInteger("State", 2);
            //animationState = 2;
        }
    }

    void EnablePlayerDoubleJump()
    {
        canDoubleJump = true;
    }

    void Fire()
    {
        if (Time.time >= timeToNextFire)
        {
            timeToNextFire = Time.time + fireRate;
            //playerAnimator.SetInteger("State", 3);      
            isFireing = true;
        if (isFacingRight)
        {
            Instantiate(rightBullet, ProjectileFirePos.position, Quaternion.identity);
        }
        else
        {
            Instantiate(leftBullet, ProjectileFirePos.position, Quaternion.identity);
        }
            
        }
        

    }
}
