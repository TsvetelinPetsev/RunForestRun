using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlayerManagerExperimental : MonoBehaviour
{



    public float speedX = 5;
    public float jumpSpeedY = 300;
    //public float delayBeforeDoubleJump = 0.01f;
    public GameObject leftBullet, rightBullet;

    float playerSpeed;
    float Horizontal;
    
    bool isFacingRight, isJumping, isOnTheGround, canDoubleJump;

    Transform firePos;
    Animator anim;
    Rigidbody2D rb;

    bool isRobot = true;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        firePos = transform.FindChild("firePos");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Horizontal = Input.GetAxis("Horizontal");
        MovePlayer();
        Flip();

        // if Up arrow key is pressed we set the jump animation and invoke the jump function
        // jump 
        if (Input.GetButtonDown("Jump") && isOnTheGround)
        {
            PlayerJump();
        }
        // double jump
        if (Input.GetButtonDown("Jump") && canDoubleJump)
        {
            PlayerJump();
        }

        if (Input.GetButtonDown("Fire"))
        {
            Fire();
        }

        // change character
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwichCharecter();

        }
    }

    private void SwichCharecter()
    {
        if (isRobot)
        {
            isRobot = false;
            anim.runtimeAnimatorController = Resources.Load("Animations/NinjaBoy/NinjaBoy") as RuntimeAnimatorController;
        }
        else
        {
            isRobot = true;
            anim.runtimeAnimatorController = Resources.Load("Animations/Robot/Robot") as RuntimeAnimatorController;
        }
    }

    void MovePlayer()
    {
        playerSpeed = Horizontal * speedX;
        // if player is moveing left or right without jumping we set animation to Running
        if (playerSpeed != 0 && !isJumping)
        {
            anim.SetInteger("State", 1);
        }

        // if player is not moving and not jumping we set the animation to Idle
        if (playerSpeed == 0 && !isJumping)
        {
            anim.SetInteger("State", 0);
        }

        //controls player forward and backword movement
        
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
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

    // if player colide with something
    void OnCollisionEnter2D(Collision2D colision)
    {
        // if player hits the ground we change the animation and state
        if (colision.gameObject.tag == "Ground")
        {
            isOnTheGround = true;
            isJumping = false;
            canDoubleJump = false;
            anim.SetInteger("State", 0);
        }

        // door teleport TODO:FIX
        if (colision.gameObject.tag == "Through")
        {
            gameObject.transform.position = new Vector3(217.5f, 7.554f, 0f);
        }



    }

    void PlayerJump()
    {
        // single jump
        if (isOnTheGround)
        {
            isOnTheGround = false;
            isJumping = true;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
            anim.SetInteger("State", 2);
            Invoke("EnablePlayerDoubleJump", 0.01f);
        }

        // double jump
        if (canDoubleJump)
        {
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
            anim.SetInteger("State", 2);
        }
    }

    void EnablePlayerDoubleJump()
    {
        canDoubleJump = true;
    }

    void Fire()
    {
        if (isFacingRight)
        {
            Instantiate(rightBullet, firePos.position, Quaternion.identity);
        }
        else
        {
            Instantiate(leftBullet, firePos.position, Quaternion.identity);
        }
    }
}

