using UnityEngine;
using System.Collections;

public class DoorAnimator : MonoBehaviour {

    public Sprite doorOpen;
    public Sprite doorClosedUnlock;
    public Sprite doorClosedLocked;
    public bool isDoorOpen;
    public bool isDoorLocked;

    private int doorStatus; // status = 0 (Open), status = 1 (closed/unlocked),status = 2 (closed/locked)
    private SpriteRenderer SpriteRenderer;

    // Use this for initialization
    void Start ()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        CheckDoorStatus();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDoorStatus();
        DoorAnimationStatus();
    }

    private void DoorAnimationStatus()
    {
        if (doorStatus == 0) // status = 0 (Open)
        {
            SpriteRenderer.sprite = doorOpen;
        }
        else if (doorStatus == 1) // status = 1 (closed/unlocked)
        {
            SpriteRenderer.sprite = doorClosedUnlock;
        }
        else if (doorStatus == 2) // status = 2 (closed/locked)
        {
            SpriteRenderer.sprite = doorClosedLocked;
        }
    }

    private void CheckDoorStatus()
    {
        if (isDoorOpen)
        {
            doorStatus = 0;
        }
        else
        {
            if (isDoorLocked)
            {
                doorStatus = 2;
            }
            else if (!isDoorLocked)
            {
                doorStatus = 1;
            }
        }
    }

    
}
