using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {

    public Vector2 speed;
    public float delay;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;
        Destroy(gameObject, delay);
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = speed;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Barrel"))
        {
            ParticleSystem exp = other.gameObject.GetComponent<ParticleSystem>();
            exp.Play();
            Destroy(other.gameObject, 0.1f);
        }
    }
}
