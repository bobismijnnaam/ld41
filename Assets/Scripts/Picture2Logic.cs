using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture2Logic : MonoBehaviour {

    public Camera mainCamera;
    public Camera observeCamera;
    public float observeDistance;
    public GameObject infoText;

    enum State {
        NotLooking,
        Looking
    }

    State state;

	// Use this for initialization
	void Start () {
		state = State.NotLooking;
	}
	
	// Update is called once per frame
	void Update () {
        switch (state) {
            case State.NotLooking:
                if (isLookingAtPicture2()) {
                    EventManager.TriggerEvent(EventManager.KEEP_INFO_ALIVE);
                }

                if (isLookingAtPicture2() && Input.GetKeyDown(KeyCode.E)) {
                    mainCamera.gameObject.SetActive(false);
                    observeCamera.gameObject.SetActive(true);

                    EventManager.TriggerEvent(EventManager.ENTER_OBSERVE_MODE);

                    state = State.Looking;
                }
                break;
            case State.Looking:
                EventManager.TriggerEvent(EventManager.KEEP_INFO_ALIVE);
                    
                if (Input.GetKeyDown(KeyCode.Q)) {
                    mainCamera.gameObject.SetActive(true);
                    observeCamera.gameObject.SetActive(false);

                    EventManager.TriggerEvent(EventManager.LEAVE_OBSERVE_MODE);

                    state = State.NotLooking;
                }
                break;
        }

	}

    bool isLookingAtPicture2() {
        var start = mainCamera.transform.position;
        var dir = mainCamera.transform.forward;
        
        RaycastHit rch;
        if (Physics.Raycast(start, dir, out rch, observeDistance)) {
            return rch.collider.gameObject.name == "Picture2";
        }

        return false;
    }
}
