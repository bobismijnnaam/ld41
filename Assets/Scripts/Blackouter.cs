using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackouter : MonoBehaviour {

    public Material blackMaterial;
    public string newTagLine;
    public GameObject tagLine;
    public Camera observeCamera;
    public AudioSource endingMusic;

    enum State {
        Idle,
        Active,
        Primed,
        Black
    }

    State state;

	// Use this for initialization
	void Start () {
	    EventManager.StartListening(EventManager.BLACKING_OUT_PHASE_START, blackingOutPhaseStart);
        EventManager.StartListening(EventManager.ENTER_OBSERVE_MODE, enterObserveMode);

        state = State.Idle;
	}

    void blackingOutPhaseStart() {
        state = State.Active;
    }

    void enterObserveMode() {
        if (state == State.Primed && !observeCamera.gameObject.activeSelf) {
            // Make painting black!
            var mr = GetComponent<MeshRenderer>();
            mr.material = blackMaterial;

            // Change tagline
            tagLine.GetComponent<TextMesh>().text = newTagLine;

            // Unprime
            state = State.Black;

            EventManager.TriggerEvent(EventManager.PICTURE_BLACKED_OUT);
        }
    }

    public void areBeingObserved() {
        if (state == State.Active) {
            state = State.Primed;
        }
    }
}
