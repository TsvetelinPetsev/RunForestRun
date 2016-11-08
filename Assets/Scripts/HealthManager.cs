using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public GameObject[] Hearts;
    public GameObject deathEffect;
        
    private int playerCurrentHealth; 
    private bool IsAlive = true;
    public bool enableCheats;

    // Use this for initialization
    void Start ()
    {
        playerCurrentHealth = Hearts.Length;
    }

    void FixedUpdate()
    {
        Cheats();
        
    }

    private void Cheats()
    {
        if (enableCheats)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                TakeDemage(1);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GetHealth(1);
            }
        }
        
    }

    public void KillPlayer()
    {
        //if PlayerDie .. reload the scene
        //TODO: make it display Death Screen
        if (IsAlive)
        {
            Instantiate(deathEffect,transform.position,transform.rotation);
            PlayerManager PlayerManager = gameObject.GetComponent<PlayerManager>();
            PlayerManager.isPlayerDeath = true;
            //Destroy(gameObject, 1);
            //SceneManager.LoadSceneAsync("Sci_Fi_Scene");
            IsAlive = false;
        }    
    }  
    
    public void TakeDemage(int takeDmg)
    {
        if (takeDmg > 0)
        {
            for (int i = 0; i < takeDmg; i++)
            {
                playerCurrentHealth--;
                if (playerCurrentHealth < 0)
                {
                    playerCurrentHealth = 0;
                }
                Hearts[playerCurrentHealth].SetActive(false);
            }
        }
        if (playerCurrentHealth<=0)
        {
            KillPlayer();
        }
        
    }

    public void GetHealth(int GetHealth)
    {
        for (int i = 1; i <= GetHealth; i++)
        {
            this.playerCurrentHealth++;
            if (playerCurrentHealth >= Hearts.Length)
            {
                playerCurrentHealth = Hearts.Length;
                Debug.Log("You cant get more helth:MaxHealth");
            }
            
            Hearts[this.playerCurrentHealth -1].SetActive(true);
            Debug.Log(playerCurrentHealth);
        }
    }
}
