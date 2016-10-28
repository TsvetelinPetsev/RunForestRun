using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public float speedX;
    public float jumpSpeedY;
    public bool IsDubleJumpAllowed;
    public float delayBeforeDoubleJump;

    float speed;
    bool isFacingRight, isJumping , isOnTheGround, canDubleJump;

    Animator anim;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(speed); // moves the player
        Flip(); // flips the player if needed

        // if Left arrow is pressed it sets the speed varible to negative speed
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed = -speedX;
        }
        // if Left arrow is not pressed sets speed varible to 0
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            speed = 0;
        }

        // if Right arrow is pressed sets the speed to positive speed
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed = speedX;
        }
        // if Right arrow is not pressed sets speed varible to 0
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            speed = 0;
        }

        // if Up arrow key is pressed we set the jump animation and invoke the jump function
        if (Input.GetKeyDown(KeyCode.UpArrow) && (isOnTheGround == true || (IsDubleJumpAllowed && canDubleJump)))
        {
            PlayerJump(); // makeing the player jump
        }

        // if Space is pressed we play Attack animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetInteger("State", 3);

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
            //canDubleJump = false;

            isJumping = false;
            anim.SetInteger("State", 0);
        }

        if (colision.gameObject.tag == "Untagged")
        {
            isJumping = false;
            anim.SetInteger("State", 0);
        }

    }

    private void PlayerJump()
    {
        // executes single jump
        if (isOnTheGround)
        {
            isOnTheGround = false;
            isJumping = true;

            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
            anim.SetInteger("State", 2);
            Invoke("EnablePlayerDoubleJump", delayBeforeDoubleJump);
        }

        // executes double jump if enabled
        if (canDubleJump)
        {
            canDubleJump = false;
            float doubleJumpSpeed = jumpSpeedY;
            if (rb.velocity.y < 0)
            {
                //Debug.Log(jumpSpeedY);
                doubleJumpSpeed = jumpSpeedY + (rb.velocity.y * -1 * rb.gravityScale);
            }
            rb.AddForce(new Vector2(rb.velocity.x, doubleJumpSpeed));
            anim.SetInteger("State", 2);
        }
    }

    private void EnablePlayerDoubleJump()
    {
        if (IsDubleJumpAllowed)
        {
            canDubleJump = true;
        }
    }
}
