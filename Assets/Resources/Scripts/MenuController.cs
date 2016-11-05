using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    private bool isPaused;
    public Button btnPause, btnPlay, btnHome;

	// Update is called once per frame
	void Update () {
        if (isPaused)
        {
            Time.timeScale = 0;
            btnPause.interactable = false;
            btnPlay.interactable = true;
        }
        else
        {
            Time.timeScale = 1;            
            btnPause.interactable = true;
            btnPlay.interactable = false;
        }
	}

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void Play()
    {
        isPaused = false;
    }

    public void Pause()
    {
        isPaused = true;

    }
    
}