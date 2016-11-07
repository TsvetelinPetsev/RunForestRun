using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour {

    public Transform playerTransform; //Player Transform
    public float cameraYOffset; //cammera Y starting offset
    //
    public float cameraSmoothing; // smooths the cammera movement
    private Vector3 cameraOffset; // camera offset
    public float MapMinimumY;     // if player falls from the map, where will camera stop at Y
    private float cameraXPosAtFall; // camera X pos when she hits the minimum Y

    //Depricated code
    //// use if cammera is ortographic to make backgroud offset the player
    //public bool enableDinamicBackground;
    //public GameObject Background;
    //public float backgroundEfectOffset = 50;
    //private float bgDinamicOffsetX;
    //private float StartPosX;
    //private float bgDinamicOffsetY;
    //private float StartPosY;



    // Use this for initialization
    void Start ()
    {
        // moves the cammera to the player first position.. aka finds the player
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + cameraYOffset * Time.deltaTime, transform.position.z);
        cameraOffset = transform.position - playerTransform.position;

        //minimumY = transform.position.y; // enable if you want the minimum camera Y position to be the player starting position
        
        // Deoricated CODE
        //dinamicBackground
        //StartPosX = playerTransform.position.x;
        //StartPosY = playerTransform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate () {

        // make camera folow the player
        Vector3 targetCamPosition = playerTransform.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position,targetCamPosition,cameraSmoothing * Time.deltaTime);

        // sets cammera position at the minimum of the map position
        if (transform.position.y < MapMinimumY)
        {
            // fixes the cammera X position to the X position that cammera was when she hit the boundery
            if (cameraXPosAtFall == 0)
            {
                cameraXPosAtFall = transform.position.x;
            }
            
            transform.position = new Vector3(cameraXPosAtFall, MapMinimumY, transform.position.z);
        }

        // Deoricated CODE
        //transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + cameraYOffset * Time.deltaTime, transform.position.z);

        ////dinamicBackground
        //if (enableDinamicBackground)
        //{
        //    bgDinamicOffsetX = StartPosX - playerTransform.position.x;
        //    StartPosX = playerTransform.position.x;

        //    bgDinamicOffsetY = StartPosY - playerTransform.position.y;
        //    StartPosY = playerTransform.position.y;

        //    //Debug.Log(bgDinamicOffsetX);
        //    Background.transform.position = new Vector3(Background.transform.position.x + (bgDinamicOffsetX / backgroundEfectOffset), Background.transform.position.y + (bgDinamicOffsetY / backgroundEfectOffset), Background.transform.position.z);
            
           
        //}
        
        
    }
}
