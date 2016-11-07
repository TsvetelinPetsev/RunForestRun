using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {
    public float destroyObjectTime;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyObjectTime);
    }
	
	
}
