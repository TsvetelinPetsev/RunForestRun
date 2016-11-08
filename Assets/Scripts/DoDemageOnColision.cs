using UnityEngine;
using System.Collections;


public class DoDemageOnColision : MonoBehaviour {

    public int Demage = 1;
    public int damageRate = 1;
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
            HealthManager.TakeDemage(Demage);
            ObjectCanDoDmg = false;
        }

        if (enableTimer)
        {
            dmgTimer += Time.deltaTime;
        }

        if (dmgTimer >= damageRate)
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
