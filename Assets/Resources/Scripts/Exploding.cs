using UnityEngine;
using System.Collections;

public class Exploding : MonoBehaviour {

    public GameObject[] DestroyObjects; //attached objects to destroy
   
    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.tag == "Projectile")
        {
            Destroy(colision.gameObject);
            foreach (GameObject obj in DestroyObjects)
            {
                Destroy(obj);
            }
            
        }
        
    }
}
