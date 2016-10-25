using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public float speedX;
    public float jumpSpeedY;
    float speed;
    bool facingRight;
    bool jumping;

    Animator anim;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(speed);
        Flip();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed = -speedX;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed = speedX;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumping = true;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetInteger("State", 3);

        }
    }

    void MovePlayer(float playerSpeed)
    {
        if (playerSpeed < 0 && !jumping || playerSpeed > 0 && !jumping)
        {
            anim.SetInteger("State", 1);
        }
        if (playerSpeed == 0 && !jumping)
        {
            anim.SetInteger("State", 0);
        }

        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    void Flip()
    {
        if (speed > 0 && !facingRight || speed < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            jumping = false;
            anim.SetInteger("State", 0);
        }
    }
}
