using UnityEngine;
using System.Collections;

public class PlayerManager_2 : MonoBehaviour {

    public float speedX;
    public float jumpSpeedY;
    public float delayBeforeDoubleJump;
    public GameObject leftBullet, rightBullet;

    float speed;
    bool isFacingRight, isJumping, isOnTheGround, canDoubleJump;

    Transform firePos;
    Animator anim;
    Rigidbody2D rb;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        firePos = transform.FindChild("firePos");

    }
	
	// Update is called once per frame
	void Update () {

        MovePlayer(speed);
        Flip();

        // if Left arrow is pressed it sets the speed varible to negative speed
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
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
            Vector3 tempVector = transform.localScale;
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
            Invoke("EnablePlayerDoubleJump", delayBeforeDoubleJump);
        }

        // double jump
        if (canDoubleJump)
        {
            canDoubleJump = false;
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
