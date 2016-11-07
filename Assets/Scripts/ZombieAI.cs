using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

    public float ZombieSpeed =5;

    Animator ZombieAnimator;
    GameObject Zombie;
    Rigidbody2D ZombieRB;

    // facing
    bool canFlip = true; // for disableing flipping while charge
    bool facingRinght = true;
    float autoFlipTime = 5f;
    float nextRandomFlip = 0f;

    //Attacking
    public float timeBeforeChargeing;
    float startChargeTime;
    bool isChargeing;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
