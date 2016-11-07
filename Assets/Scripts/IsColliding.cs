using UnityEngine;
using System.Collections;

public class IsColliding : MonoBehaviour {

    public bool IsObjectTriggerCollideing;
    public bool IsObjectColiderCollideing;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D Colider)
    {
        IsObjectTriggerCollideing = true;
    }

    void OnTriggerExit2D(Collider2D Colider)
    {
        IsObjectTriggerCollideing = false;
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        IsObjectColiderCollideing = true;
    }
    void OnCollisionExit2D(Collision2D colision)
    {
        IsObjectColiderCollideing = false;
    }

}
