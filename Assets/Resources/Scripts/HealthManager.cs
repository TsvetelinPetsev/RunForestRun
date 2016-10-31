using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public GameObject[] Hearts;
    private int health;
    private int money;

	// Use this for initialization
	void Start () {
        health = Hearts.Length;
	}
	
	// Update is called once per frame
	void Update () {
	
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

        if (other.gameObject.CompareTag("Coin"))
        {
            money++;
            GameObject.Find("MoneyTxt").GetComponent<Text>().text = money.ToString();
            Destroy(other.gameObject);
        }
    }
}
