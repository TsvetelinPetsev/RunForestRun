using UnityEngine;
using System.Collections;

public class RocketProjectileManager : MonoBehaviour
{
    
    public Vector2 velocity;
    public float destroyRocketDelay;
    public float smokeSetleTime;
    public bool canExplode = true;

    public GameObject ExplosionEffect;
    Rigidbody2D rb,childrenRB;

    float RocketLounchTime;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
        childrenRB = GetComponentInChildren<Rigidbody2D>();
        RocketLounchTime = Time.time;
        //Destroy(gameObject, destroyRocketDelay + 1 );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = velocity;
        DestroyRocket(destroyRocketDelay);
    }

    public void DestroyRocket(float delay)
    {
        if (Time.time >= RocketLounchTime + delay)
        {
            SetRocketForce(0f, 0f);
            if (canExplode)
            {
                Instantiate(ExplosionEffect, transform.position, transform.rotation);
                canExplode = false;
            }
            Destroy(gameObject);
        }
    }

    public void SetRocketForce(float velocityX, float velocityY)
    {
        rb.velocity = new Vector2(velocityX, velocityY);
        childrenRB.velocity = new Vector2(velocityX, velocityY);


    }

}
