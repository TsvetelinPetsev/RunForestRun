using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {

    public Transform player; //Player Transform
    public float cameraYOffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // make camera folow the player
        transform.position = new Vector3(player.position.x, player.position.y + cameraYOffset, transform.position.z); // or just use Vector2
	}
}
