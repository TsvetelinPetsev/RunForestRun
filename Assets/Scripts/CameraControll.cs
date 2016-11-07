using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {

    public Transform player; //Player Transform
    public float cameraYOffset;    
    public bool enableDinamicBackground;
    public GameObject Background;
    public float backgroundEfectOffset = 50;
    private float bgDinamicOffsetX;
    private float StartPosX;
    private float bgDinamicOffsetY;
    private float StartPosY;
    // Use this for initialization
    void Start ()
    {
        StartPosX = player.position.x;
        StartPosY = player.position.y;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // make camera folow the player
        
        transform.position = new Vector3(player.position.x, player.position.y + cameraYOffset * Time.deltaTime, transform.position.z);


        if (enableDinamicBackground)
        {
            bgDinamicOffsetX = StartPosX - player.position.x;
            StartPosX = player.position.x;

            bgDinamicOffsetY = StartPosY - player.position.y;
            StartPosY = player.position.y;

            //Debug.Log(bgDinamicOffsetX);
            Background.transform.position = new Vector3(Background.transform.position.x + (bgDinamicOffsetX / backgroundEfectOffset), Background.transform.position.y + (bgDinamicOffsetY / backgroundEfectOffset), Background.transform.position.z);
            
           
        }
        
        
    }
}
