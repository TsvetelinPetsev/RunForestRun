using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadSceneAsync("Menu");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
