using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public GameObject[] Hearts;
    private int health;
    public int acidDmgTimer = 1;
    private float dmgTimer = 0;
    private bool acidCanTakeDmg;
    private bool enableTimerAcid;

	// Use this for initialization
	void Start () {
        health = Hearts.Length;
        

    }

    void FixedUpdate()
    {
        onAcidTakeDMG();
        IsDeath();
    }

    private void IsDeath()
    {
        if (health <= 0)
        {
            SceneManager.LoadSceneAsync("Sci_Fi_Scene");
        }
    }

    private void onAcidTakeDMG()
    {
        if (acidCanTakeDmg)
        {
            TakeDMG(1);
            acidCanTakeDmg = false;
        }
        if (enableTimerAcid)
        {
            dmgTimer += Time.deltaTime;
        }


        if (dmgTimer >= acidDmgTimer)
        {
            acidCanTakeDmg = true;
            dmgTimer = 0;
        }
        else
        {
            acidCanTakeDmg = false;
        }
    }

    void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.CompareTag("Acid"))
        {
            acidCanTakeDmg = true;
            enableTimerAcid = true;
        }
    }

    void OnTriggerExit2D(Collider2D colision)
    {
        
        if (colision.gameObject.CompareTag("Acid"))
        {
            acidCanTakeDmg = false;
            enableTimerAcid = false;
        }
    }


    void TakeDMG(int takeDmg)
    {
        // TODO: take more then 1 dmg properly
        health -= takeDmg;
        Hearts[health].SetActive(false);
    }
}
