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

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Ground"))
        {
            Destroy(colision.gameObject);
            Destroy(gameObject);
        }
    }
}
