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

    void OnCollisionEnter2D(Collision2D other)
    {   
        if (other.gameObject.CompareTag("Coin"))
        {
            money++;
            GameObject.Find("MoneyTxt").GetComponent<Text>().text = money.ToString();
            Destroy(other.gameObject);
        }
    }
}
