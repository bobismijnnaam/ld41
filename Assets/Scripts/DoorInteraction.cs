using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour {

    public const float OBSERVE_DISTANCE = 3f;

    AudioSource doorknobAudio;
    Camera mainCamera;
    Text infoText;

	// Use this for initialization
	void Start () {
        doorknobAudio = GetComponent<AudioSource>();

        mainCamera = Camera.main;
        if (mainCamera == null) {
            Debug.LogError("No main camera!");
        }

        infoText = GameObject.Find("InfoText").GetComponent<Text>();
        if (infoText == null) {
            Debug.LogError("Cannot find InfoText variable!");
        }

        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (isLookingAtMe()) {
            infoText.text = "Press E to interact";
            EventManager.TriggerEvent(EventManager.KEEP_INFO_ALIVE);

            if (Input.GetKeyDown(KeyCode.E)) {
                if (!doorknobAudio.isPlaying) {
                    EventManager.TriggerEvent(EventManager.KNOB_TWISTED);
                    doorknobAudio.Play();
                }
            }   
        }
	}

    bool isLookingAtMe() {
        var start = mainCamera.transform.position;
        var dir = mainCamera.transform.forward;
        
        RaycastHit rch;
        if (Physics.Raycast(start, dir, out rch, OBSERVE_DISTANCE)) {
            var parent = rch.collider.gameObject.transform.parent;
            if (parent != null) {
                return parent.gameObject.name == gameObject.name;
            }
        }

        return false;
    }
}
