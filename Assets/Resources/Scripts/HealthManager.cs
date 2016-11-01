using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public GameObject[] Hearts;
    private int health;

	// Use this for initialization
	void Start () {
        health = Hearts.Length;
	}
	
	

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Barrel"))
        {
            health--;
            Hearts[health].SetActive(false);
        }
        if (health == 0)
        {
            SceneManager.LoadSceneAsync("Sci_Fi_Scene");
        }
       
    }
}
