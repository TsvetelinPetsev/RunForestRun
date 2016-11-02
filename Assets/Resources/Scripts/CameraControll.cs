using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {

    public Transform player; //Player Transform
    public float cameraYOffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // make camera folow the player
        transform.position = new Vector3(player.position.x, player.position.y + cameraYOffset * Time.deltaTime, transform.position.z); 
	}
}
