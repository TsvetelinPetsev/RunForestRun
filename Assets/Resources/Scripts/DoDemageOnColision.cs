using UnityEngine;
using System.Collections;


public class DoDemageOnColision : MonoBehaviour {

    public int howMuchDMG = 1;
    public int damageTimer = 1;
    public bool doDMGAtStart = true;
    private float dmgTimer = 0;
    private bool ObjectCanDoDmg;
    private bool enableTimer;

    HealthManager HealthManager;
    // Use this for initialization
    void Start ()
    {
        HealthManager = GameObject.FindObjectOfType(typeof(HealthManager)) as HealthManager;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        ObjectDoDMG();
    }

    private void ObjectDoDMG()
    {
        if (ObjectCanDoDmg)
        {
            HealthManager.TakeDMG(howMuchDMG);
            ObjectCanDoDmg = false;
        }

        if (enableTimer)
        {
            dmgTimer += Time.deltaTime;
        }

        if (dmgTimer >= damageTimer)
        {
            ObjectCanDoDmg = true;
            dmgTimer = 0;
        }
        else
        {
            ObjectCanDoDmg = false;
        }
    }

    void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.CompareTag("Player"))
        {
            if (doDMGAtStart)
            {
                ObjectCanDoDmg = true;
            }            
            enableTimer = true;
        }
    }

    void OnTriggerExit2D(Collider2D colision)
    {

        if (colision.gameObject.CompareTag("Player"))
        {
            ObjectCanDoDmg = false;
            enableTimer = false;
        }
    }
}
