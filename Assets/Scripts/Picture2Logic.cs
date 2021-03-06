﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture2Logic : MonoBehaviour {

    public AudioClip notificationSound;
    public Camera mainCamera;
    public Camera observeCamera;
    public float observeDistance;
    public GameObject infoText;
    public GameObject[] doors;
    public Material emptyBowlMaterial;
    public GameObject frame3;
    public GameObject pic2tagline;

    enum State {
        NotLooking,
        Looking
    }

    State state;
    bool triggeredDoors;
    bool picture3Started;
    bool hasPlayedLaughterTrack;
    AudioSource dingAndWalkAudio;
    AudioSource laughterAudio;
    Blackouter blackouter;

	// Use this for initialization
	void Start () {
        if (doors.Length != 2) {
            Debug.LogError("Length of doors array does not equal 2!");
        }

        foreach (var audio in GetComponents<AudioSource>()) {
            if (audio.clip.name == "ding_walk") {
                dingAndWalkAudio = audio;
            } else {
                laughterAudio = audio;
            }
        }

		state = State.NotLooking;

        dingAndWalkAudio = GetComponent<AudioSource>();

        EventManager.StartListening(EventManager.KNOB_TWISTED, knobTwisted);

        blackouter = GetComponentsInChildren<Blackouter>()[0];
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

                    if (!triggeredDoors) {
                        triggeredDoors = true;
                        startDoorScene();
                    }

                    if (triggeredDoors && hasPlayedLaughterTrack && !picture3Started) {
                        picture3Started = true;
                        startPicture3();
                    }

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

    void startDoorScene() {
        foreach (var door in doors) {
            door.SetActive(true);
        }

        dingAndWalkAudio.PlayDelayed(1.5f);
    }

    void startPicture3() {
        foreach (var door in doors) {
            door.SetActive(false);
        }
        frame3.SetActive(true);
        ringBell(frame3.transform.position);
    }

    void knobTwisted() {
        if (!hasPlayedLaughterTrack) {
            hasPlayedLaughterTrack = true;

            // Play laughter
            laughterAudio.Play();

            // Set empty bowl material (STEAL FRUIT!)
            var pic2 = GameObject.Find("Picture2").gameObject;
            var meshRenderer = pic2.GetComponent<MeshRenderer>();
            meshRenderer.material = emptyBowlMaterial;

            // Set tag line
            pic2tagline.GetComponent<TextMesh>().text = "Bowl";
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

    void ringBell(Vector3 pos) {
        AudioSource.PlayClipAtPoint(notificationSound, pos);
    }
}
