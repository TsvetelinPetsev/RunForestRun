using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour {

    public int EnemyMaxHealth;
    private int EnemyCurrentHealth;
    public GameObject EnemyDeathFX;
    public Slider enemyHealthSlider;

	// Use this for initialization
	void Start ()
    {
        EnemyCurrentHealth = EnemyMaxHealth;
        enemyHealthSlider.maxValue = EnemyMaxHealth;
        enemyHealthSlider.value = EnemyCurrentHealth;

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void DemageEnemy(int demage)
    {
        EnemyCurrentHealth -= demage;

        enemyHealthSlider.gameObject.SetActive(true);
        enemyHealthSlider.value = EnemyCurrentHealth;

        if (EnemyCurrentHealth <= 0)
        {
            EnemyCurrentHealth = 0;
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        if (EnemyDeathFX != null)
        {
            
        }
        
        Destroy(gameObject);
        Instantiate(EnemyDeathFX, transform.position + (transform.up * -1), transform.rotation);
    }
}
