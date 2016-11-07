using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlayerManager : MonoBehaviour {



    public float speedX = 5;
    public float jumpSpeedY = 300;
    
    //public float delayBeforeDoubleJump = 0.01f;
    
    public LayerMask GroundLayer;
    
    float playerSpeed;
    float Horizontal;

    bool isFacingRight,isOnTheGround, canDoubleJump;

    
    Animator playerAnimator;
    Rigidbody2D playerRigidBody;
    GameObject GroundedTrigger;

    bool isRobot = true;

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
        
        Horizontal = Input.GetAxis("Horizontal");
        MovePlayer();
        Flip();

        
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

    // if player colide with something
    void OnCollisionEnter2D(Collision2D colision)
    {
        // if player is on top of and object or ground, we change the animation state and enable jumping
        if (GroundedTrigger.GetComponent<IsColliding>().IsObjectTriggerCollideing)
        {
            playerAnimator.SetInteger("State", 0);
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
        if (playerSpeed != 0 && isOnTheGround)
        {
            playerAnimator.SetInteger("State", 1);
        }

        // if player is not moving and not jumping we set the animation to Idle
        if (playerSpeed == 0 && isOnTheGround)
        {
            playerAnimator.SetInteger("State", 0);
        }

        //controls player forward and backword movement

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
            playerAnimator.SetInteger("State", 2);
            Invoke("EnablePlayerDoubleJump", 0.01f);
        }

        // double jump
        if (canDoubleJump)
        {
            canDoubleJump = false;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0);
            playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x, jumpSpeedY));
            playerAnimator.SetInteger("State", 2);
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
           playerAnimator.SetInteger("State", 3);
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
