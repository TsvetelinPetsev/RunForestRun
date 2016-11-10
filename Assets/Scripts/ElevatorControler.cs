using UnityEngine;
using System.Collections;

public class ElevatorControler : MonoBehaviour {

    public float maxPosY = 5;                       // maximum for elevator's Y
    public float maxElevatorVelocity = 1;           // how fast will elevator go UP/Down
    public float timeStayDown = 1;                  // how much time to stay at Down position
    public float timeStayUp = 1;                    // how much time to stay at Down position
    public bool enableElevatorSmoothness;           // enable/disable elevator smoothness option
    public float smoothness = 100;                  // how fast it will get to the maximum velocity
    public float SmoothnessStartingPoint = 0.5f;    // what is the starting velocity percentage .. 0.5 = 50%

    float currentElevatorVelocity;      // current elevator velocity
    float elevatorStartPositionY;        // elevator start position
    float elevatorWhaitTimerUp;         // timer that counts how much more time will the elevator be at top position
    float elevatorWhaitTimerDown;       // timer that counts how much more time will the elevator be at bottom position
    float elevatorSmoothVelocity = 1;

    private Transform elevatorTransform; // will hold elevator transform

    AudioSource elevatorSound;

    void Start ()
    {
        elevatorTransform = GetComponent<Transform>(); // geting elevator transform
        elevatorStartPositionY = elevatorTransform.position.y;  // setting elevator starting position
        elevatorWhaitTimerUp = timeStayUp;                     // setting up timer 
        elevatorWhaitTimerDown = timeStayDown;                 // setting down timer 
        elevatorSound = GetComponentInChildren<AudioSource>();
    }
	
	
    void FixedUpdate()
    {
        MoveElevator(currentElevatorVelocity); // moveing the elevator
        ElevatorPositionController(); // setting up where will the elevator move next
    }

    void ElevatorPositionController()
    {

        // if elevator is at max position, we change velocity direction
        if (elevatorTransform.position.y >= maxPosY) 
        {
            elevatorTransform.transform.position = new Vector2(elevatorTransform.position.x, maxPosY); // fix elevator going upper then needed
            if (elevatorWhaitTimerUp <= 0) // checking if elevator shoud whait at top position
            {
                currentElevatorVelocity = maxElevatorVelocity * -1 ;
                elevatorWhaitTimerUp = timeStayUp;
                
                //if smoothness is enabled, lower starting velocity
                if (enableElevatorSmoothness)
                {
                    elevatorSmoothVelocity = SmoothnessStartingPoint;
                }
            }
            else
            {
                currentElevatorVelocity = 0;                
            }

            elevatorWhaitTimerUp -= Time.deltaTime;
           
        }

        // if elevator is at max position, we change velocity direction
        if (elevatorTransform.position.y <= elevatorStartPositionY)
        {
            elevatorTransform.transform.position = new Vector2(elevatorTransform.position.x, elevatorStartPositionY); // fix the elevetor going lower then needed

            if (elevatorWhaitTimerDown <= 0) // checking if elevator shoud whait at bottom position
            {
                currentElevatorVelocity = maxElevatorVelocity;
                elevatorWhaitTimerDown = timeStayDown;
                if (enableElevatorSmoothness)
                {
                    elevatorSmoothVelocity = SmoothnessStartingPoint;
                }
                
            }
            else
            {
                currentElevatorVelocity = 0;
            }

            elevatorWhaitTimerDown -= Time.deltaTime;
        }

    }

    void MoveElevator(float elevatorSpeed)
    {
        // if smoothness is enabled we smoothly change the velocity to max velocity
        if (enableElevatorSmoothness)
        {
            if (elevatorSmoothVelocity < 1)
            {
                elevatorSmoothVelocity += elevatorSmoothVelocity / smoothness;
            }
            else if (elevatorSmoothVelocity >= 1)
            {
                elevatorSmoothVelocity = 1;
            }
            
        }

        // moveing the elevator
        elevatorTransform.position = new Vector2(elevatorTransform.position.x,elevatorTransform.position.y + (elevatorSpeed * elevatorSmoothVelocity *  Time.deltaTime));
    }

    // if player colide with something
    void OnCollisionEnter2D(Collision2D colision)
    {
        // if elevator colide with the player we get him as a child and move him relativly to the elevator
        if (colision.gameObject.tag == "Player")
        {            
            colision.gameObject.transform.parent = gameObject.transform;
            elevatorSound.Play();
        }
        


    }

    void OnCollisionExit2D(Collision2D colision)
    {
        // if we got the colision object as a child, move him back to his place
        colision.gameObject.transform.parent = null;
        elevatorSound.Stop();
    }
}
