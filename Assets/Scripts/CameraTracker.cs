using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(gameObject.transform.eulerAngles);
        Debug.Log(gameObject.transform.forward);

        RaycastHit rch;
        var hit = Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out rch, 3);
        if (hit && rch.collider.gameObject.name == "Picture") {
            EventManager.TriggerEvent(EventManager.SEE_PICTURE);
        } else {
            EventManager.TriggerEvent(EventManager.SEE_NOTHING);
        }
	}
}
