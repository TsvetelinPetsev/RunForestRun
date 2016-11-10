using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {
       
    private int money;
    private int highScore;
    AudioSource coinMusic;
    public AudioClip coinClip;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore");
        GameObject.Find("BestTimeScore").GetComponent<Text>().text = highScore.ToString();
        coinMusic = GetComponentInChildren<AudioSource>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            money++;
            GameObject.Find("MoneyTxt").GetComponent<Text>().text = money.ToString();
        }

        if (money > highScore)
        {
            PlayerPrefs.SetInt("highscore", money);
            PlayerPrefs.Save();
            GameObject.Find("BestTimeScore").GetComponent<Text>().text = money.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D colision)
    {   
        if (colision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Triggered");
            money++;
            GameObject.Find("MoneyTxt").GetComponent<Text>().text = money.ToString();

            coinMusic.clip = coinClip;
            coinMusic.PlayOneShot(coinClip, 0.3f);

            GameObject.Find("YourTimeScore").GetComponent<Text>().text = money.ToString();
            GameObject.Find("BestTimeScore").GetComponent<Text>().text = highScore.ToString();
            Destroy(colision.gameObject);
        }
    }

    public void ResetScore()
    {
        //money = 0;
        highScore = 0;
        GameObject.Find("BestTimeScore").GetComponent<Text>().text = highScore.ToString();
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.Save();
    }
}
