using UnityEngine;
using System.Collections;

public class OnProjectileHit : MonoBehaviour {

    public float Demage;
    private RocketProjectileManager projectileController;

    public GameObject ExplosionEffect;
    
    // Use this for initialization
    void Awake ()
    {
        projectileController = GetComponentInParent<RocketProjectileManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D colider)
    {
        Debug.Log("missle hit");
        if (colider.gameObject.layer == LayerMask.NameToLayer("ShootableObjects"))
        {
            //Debug.Log("shootable");
            projectileController.SetRocketForce(0f,0f);
            projectileController.canExplode = false;
            Instantiate(ExplosionEffect,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D colider)
    {
        if (colider.gameObject.layer == LayerMask.NameToLayer("ShootableObjects"))
        {
            projectileController.SetRocketForce(0f, 0f);
            projectileController.canExplode = false;
            Instantiate(ExplosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
