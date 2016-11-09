using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

    public float ZombieSpeed =5;

    Animator ZombieAnimator;
    public GameObject Zombie;
    Rigidbody2D ZombieRB;

    // facing
    bool canFlip = true; // for disableing flipping while charge
    bool facingRinght = true;
    float autoFlipTime = 5f;
    float nextRandomFlip = 0f;

    //Attacking
    public float timeBeforeChargeing;
    float startChargeTime;
    bool isChargeing;

    // Use this for initialization
    void Start ()
    {
        ZombieAnimator = GetComponentInChildren<Animator>();
        ZombieRB = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > nextRandomFlip)
        {
            if (Random.Range(0, 10) >= 5)
            {
                flipFacing();
            }

            nextRandomFlip = Time.time + autoFlipTime;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!facingRinght && other.transform.position.x > transform.position.x)
            {
                flipFacing();
            }
            else if (facingRinght && other.transform.position.x < transform.position.x)
            {
                flipFacing();
            }

            canFlip = false;
            isChargeing = true;
            startChargeTime = Time.time + timeBeforeChargeing;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (startChargeTime < Time.time)
            {
                if (facingRinght)
                {
                    ZombieRB.AddForce(new Vector2(1, 0) * ZombieSpeed);
                }
                else
                {
                    ZombieRB.AddForce(new Vector2(-1, 0) * ZombieSpeed);
                    ZombieAnimator.SetInteger("State", 2);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFlip = true;
            isChargeing = false;
            ZombieRB.velocity = new Vector2(0f, 0f);
            ZombieAnimator.SetInteger("State", 0);
        }
    }

    void flipFacing()
    {
        if (!canFlip)
        {
            return;
        }

        float facingX = Zombie.transform.localScale.x;
        facingX *= -1f;
        Zombie.transform.localScale = new Vector3(facingX, Zombie.transform.localScale.y, Zombie.transform.localScale.z);
        facingRinght = !facingRinght; 
    }
}
