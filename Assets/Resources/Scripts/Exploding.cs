using UnityEngine;
using System.Collections;

public class Exploding : MonoBehaviour {

    public GameObject[] DestroyObjects; //attached objects to destroy
    private ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

        void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.tag == "Projectile")
        {
            Destroy(colision.gameObject);

            particle.Play();
            Destroy(gameObject, particle.duration);
            foreach (GameObject obj in DestroyObjects)
            {
                Destroy(obj);
            }
            
        }        
        
    }
}
