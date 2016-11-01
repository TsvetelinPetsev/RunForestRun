using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {
       
    private int money;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            money++;
            GameObject.Find("MoneyTxt").GetComponent<Text>().text = money.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D colision)
    {   
        if (colision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Triggered");
            money++;
            GameObject.Find("MoneyTxt").GetComponent<Text>().text = money.ToString();
            Destroy(colision.gameObject);
        }
    }
}
