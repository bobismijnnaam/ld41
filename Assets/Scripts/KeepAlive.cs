using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAlive : MonoBehaviour {

    public float aliveDuration = 0.1f;
    float startTime;

	// Use this for initialization
	void Start () {
		EventManager.StartListening(EventManager.KEEP_INFO_ALIVE, keepInfoAlive);
        startTime = -99999f;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    gameObject.SetActive((Time.time - startTime) < aliveDuration);
	}

    void keepInfoAlive() {
        startTime = Time.time;
        gameObject.SetActive(true);
    }
}
