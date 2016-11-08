using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int EnemyMaxHealth;
    private int EnemyCurrentHealth;

	// Use this for initialization
	void Start ()
    {
        EnemyCurrentHealth = EnemyMaxHealth;


    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void DemageEnemy(int demage)
    {
        EnemyCurrentHealth -= demage;
        if (EnemyCurrentHealth <= 0)
        {
            EnemyCurrentHealth = 0;
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }
}
