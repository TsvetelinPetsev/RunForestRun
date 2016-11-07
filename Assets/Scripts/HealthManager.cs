using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public GameObject[] Hearts;
        
    private int health; 
    private bool enableLoadOnce = true;
    public bool enableCheats;

    // Use this for initialization
    void Start ()
    {
        health = Hearts.Length;
    }

    void FixedUpdate()
    {
        Cheats();
        IsDeath();
    }

    private void Cheats()
    {
        if (enableCheats)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                TakeDMG(1);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GetHealth(1);
            }
        }
        
    }

    private void IsDeath()
    {
        if (health <= 0)
        {
            if (enableLoadOnce)
            {
                SceneManager.LoadSceneAsync("Sci_Fi_Scene");
                enableLoadOnce = false;
            }
            
        }
    }  
    
    public void TakeDMG(int takeDmg)
    {
        for (int i = 0; i < takeDmg; i++)
        {
            health--;               
            Hearts[health].SetActive(false);
        } 
    }

    public void GetHealth(int GetHealth)
    {
        for (int i = 1; i <= GetHealth; i++)
        {
            this.health++;
            if (health >= Hearts.Length)
            {
                health = Hearts.Length;
                Debug.Log("You cant get more helth:MaxHealth");
            }
            
            Hearts[this.health -1].SetActive(true);
            Debug.Log(health);
        }
    }
}
