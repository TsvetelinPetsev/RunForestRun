﻿using UnityEngine;
using System.Collections;

public class DoorEnterExit : MonoBehaviour {

    private bool IsAtTheDoor, IsInside;
    private GameObject Player;
    private SpriteRenderer PlayerSpriteRenderer;
    private DoorAnimator DoorAnimator;
    public GameObject RoomWalls;

    private string PlayerLayer = "Player";
    private string PlayerRoomLayer = "PlayerInRoom";
    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Robot");
        PlayerSpriteRenderer = Player.GetComponent<SpriteRenderer>();
        DoorAnimator = GetComponent<DoorAnimator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Interact") && PlayerSpriteRenderer && IsAtTheDoor)
        {
            // if the door is unlocked
            if (!DoorAnimator.isDoorLocked)
            {
                //opening the door animation 
                DoorAnimator.isDoorOpen = true;

                //get the player inside/outside
                SwichRooms();
            }
            
            
        }
	}

    void SwichRooms()
    {


        if (PlayerSpriteRenderer.sortingLayerName == PlayerLayer)
        {
            // Going inside
            IsInside = true;            
            PlayerSpriteRenderer.sortingLayerName = PlayerRoomLayer;
            RoomWalls.SetActive(true);
        }
        else if(PlayerSpriteRenderer.sortingLayerName == PlayerRoomLayer)
        {
            //Going outside
            IsInside = false;
            PlayerSpriteRenderer.sortingLayerName = PlayerLayer;
            RoomWalls.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D Colider)
    {
        if (Colider.CompareTag("Player"))
        {
            Debug.Log("Player at the door");
            IsAtTheDoor = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D Colider)
    {
        if (Colider.CompareTag("Player"))
        {
            IsAtTheDoor = false;
            //closing the door animation 
            DoorAnimator.isDoorOpen = false;
        }
    }
}
