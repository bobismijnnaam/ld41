using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture3Logic : MonoBehaviour {

    public const float OBSERVE_DISTANCE = 3f;

    public Camera observeCamera;

    enum State {
        NotLooking,
        Looking
    }

    Blackouter blackouter;
    Camera mainCamera;
    State state;

    bool hasBlackoutStarted;

	// Use this for initialization
	void Start () {
		state = State.NotLooking;
        mainCamera = Camera.main;
        gameObject.SetActive(false);
        blackouter = GetComponentsInChildren<Blackouter>()[0];
	}
	
	// Update is called once per frame
	void Update () {
        switch (state) {
            case State.NotLooking:
                if (isLookingAtPicture3()) {
                    EventManager.TriggerEvent(EventManager.KEEP_INFO_ALIVE);
                }

                if (isLookingAtPicture3() && Input.GetKeyDown(KeyCode.E)) {
                    mainCamera.gameObject.SetActive(false);
                    observeCamera.gameObject.SetActive(true);

                    if (!hasBlackoutStarted) {
                        EventManager.TriggerEvent(EventManager.BLACKING_OUT_PHASE_START);
                        hasBlackoutStarted = true;
                    }

                    EventManager.TriggerEvent(EventManager.ENTER_OBSERVE_MODE);
                    state = State.Looking;
                    blackouter.areBeingObserved();
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

    bool isLookingAtPicture3() {
        var start = mainCamera.transform.position;
        var dir = mainCamera.transform.forward;
        
        RaycastHit rch;
        if (Physics.Raycast(start, dir, out rch, OBSERVE_DISTANCE)) {
            return rch.collider.gameObject.name == "Picture3";
        }

        return false;
    }
}
