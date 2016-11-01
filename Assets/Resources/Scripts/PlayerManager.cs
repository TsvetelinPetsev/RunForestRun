using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlayerManager : MonoBehaviour {


    public float speedX = 5;
    public float jumpSpeedY = 300;
    //public float delayBeforeDoubleJump = 0.01f;
    public GameObject leftBullet, rightBullet;

    float speed;
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
    void Update()
    {

        MovePlayer(speed);
        Flip();

        // if Left arrow is pressed it sets the speed varible to negative speed
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //anim.runtimeAnimatorController = Resources.Load("Animations/Robot/Robot..controller") as RuntimeAnimatorController;
            speed = -speedX + Time.deltaTime;
        }
        
        // if Left arrow is not pressed sets speed varible to 0
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            speed = 0;
        }

        // if Right arrow is pressed sets the speed to positive speed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed = speedX + Time.deltaTime;
        }

        // if Right arrow is not pressed sets speed varible to 0
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            speed = 0;
        }

        // if Up arrow key is pressed we set the jump animation and invoke the jump function
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnTheGround)
        {
            PlayerJump();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canDoubleJump)
        {
            PlayerJump();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            Fire();
        }

        // change character
        if (Input.GetKeyDown(KeyCode.W))
        {
            //anim.runtimeAnimatorController = Instantiate(Resources.Load("Animation/Robot") as RuntimeAnimatorController);
            //anim.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animations/NinjaBoy/NinjaBoy", typeof(RuntimeAnimatorController)));
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
    }

    void MovePlayer(float playerSpeed)
    {
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

        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    // fliping the player if needed
    void Flip()
    {
        if (speed > 0 && !isFacingRight || speed < 0 && isFacingRight)
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
