using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public GameObject[] Hearts;
    public GameObject deathEffect;

    public int PlayerFullHealth;    
    private int playerCurrentHealth; 
    private bool IsAlive = true;
    public bool enableCheats;

    //HUD varibles
    public Slider healthSlider;

    // dameged splat screen
    public Image damageScreenImage;
    private bool playerDamaged = false;
    Color damageColour = new Color(1f,0f,0f,1f); // change image alpha
    float smoothColourOverTime = 5f;

    // Use this for initialization
    void Start ()
    {
        PlayerFullHealth = Hearts.Length;
        playerCurrentHealth = PlayerFullHealth;

        // grab the HUD elements and inicialize
        healthSlider.maxValue = PlayerFullHealth;
        healthSlider.value = playerCurrentHealth;
    }

    void FixedUpdate()
    {
        DamagedScreen();
    }

    private void DamagedScreen()
    {
        Cheats();
        if (playerDamaged)
        {
            damageScreenImage.color = damageColour;
        }
        else
        {
            damageScreenImage.color = Color.Lerp(damageScreenImage.color, Color.clear, smoothColourOverTime * Time.deltaTime);
        }
        playerDamaged = false;
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
            Invoke("ReloadScene", 3);            
            IsAlive = false;
        }    
    }

    private void ReloadScene()
    {
        SceneManager.LoadSceneAsync("Sci_Fi_Scene");
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

                playerDamaged = true;
                Hearts[playerCurrentHealth].SetActive(false);

                healthSlider.value = playerCurrentHealth;
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
