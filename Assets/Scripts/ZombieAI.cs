using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

    public float ZombieSpeed = 5;

    Animator ZombieAnimator;
    public GameObject Zombie;
    Rigidbody2D ZombieRB;

    // facing
    //bool canFlip = true; // for disableing flipping while charge
    public bool facingRinght = true;
    float autoFlipTime = 5f;
    float nextRandomFlip = 0f;

    //Attacking
    public float timeBeforeChargeing;
    float startChargeTime;
    bool isChargeing;
    bool isEnemyAlive = true;
    EnemyMeleeDMG enemyMelleDMGScript;
    EnemyHealth enemyhealthScript;
    // Use this for initialization
    void Start ()
    {
        ZombieAnimator = GetComponentInChildren<Animator>();
        enemyMelleDMGScript = GetComponentInChildren<EnemyMeleeDMG>();
        enemyhealthScript = GetComponentInChildren<EnemyHealth>();
        ZombieRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!isChargeing)
        {
            if (Time.time > nextRandomFlip)
            {
                flipFacing();
                nextRandomFlip = Time.time + Random.Range(2, 10);
            }
        }
        if (enemyhealthScript.isEnemyDeath && isEnemyAlive)
        {
            ZombieAnimator.SetInteger("State", 3);
            Destroy(gameObject, 6);
            isEnemyAlive = false;
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isEnemyAlive)
        {

           
            if (facingRinght && other.transform.position.x < transform.position.x)
            {
                flipFacing();
            }
            else if (!facingRinght && other.transform.position.x > transform.position.x)
            {
                flipFacing();
            }                        
            isChargeing = true;
            startChargeTime = Time.time + timeBeforeChargeing;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isEnemyAlive)
        {
            if (startChargeTime < Time.time)
            {
                if (enemyMelleDMGScript.isPlayerDemaged)
                {

                    ZombieAnimator.SetInteger("State", 2);
                }
                else
                {
                    if (facingRinght)
                    {
                        //ZombieRB.AddForce(new Vector2(1, 0) * ZombieSpeed);
                        ZombieRB.velocity = new Vector2(ZombieSpeed * Time.deltaTime, ZombieRB.velocity.y);
                        ZombieAnimator.SetInteger("State", 1);
                    }
                    else
                    {
                        //ZombieRB.AddForce(new Vector2(-1, 0) * ZombieSpeed);
                        ZombieRB.velocity = new Vector2(-ZombieSpeed * Time.deltaTime, ZombieRB.velocity.y);
                        ZombieAnimator.SetInteger("State", 1);
                    }
                }
                
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isEnemyAlive)
        {
            
            isChargeing = false;
            ZombieRB.velocity = new Vector2(0f, 0f);
            ZombieAnimator.SetInteger("State", 0);
        }
    }

    void flipFacing()
    {
        if (isEnemyAlive)
        {
            float facingX = Zombie.transform.localScale.x;
            facingX *= -1f;
            Zombie.transform.localScale = new Vector3(facingX, Zombie.transform.localScale.y, Zombie.transform.localScale.z);
            facingRinght = !facingRinght;
        }
                   
    }
}
