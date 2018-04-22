using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRevealer : MonoBehaviour {

    public GameObject[] toDisable;
    public GameObject[] toDisableAfterSouth;
    public GameObject[] toEnable;
    public Material starSkybox;
    public GameObject endText;

    int blackedOutPictureCount = 0;
    float startTime;
    const float END_TEXT_WAIT_DURATION = 3f;

    enum State {
        Idle,
        PrimedForMainWallDeletion,
        WaitingForEndText,
        Done
    }

    State state;
    bool primedForMainWallDeletion;

	// Use this for initialization
	void Start () {
		EventManager.StartListening(EventManager.PICTURE_BLACKED_OUT, pictureBlackedOut);
        EventManager.StartListening(EventManager.SEE_SOUTH_WALL, seeSouthWall);
        state = State.Idle;
	}

    void pictureBlackedOut() {
        blackedOutPictureCount += 1;

        if (blackedOutPictureCount == 3) {
            foreach (var obj in toDisable) {
                obj.SetActive(false);
            }

            foreach (var obj in toEnable) {
                obj.SetActive(true);
            }

            state = State.PrimedForMainWallDeletion;

            RenderSettings.skybox = starSkybox;
        }
    }

    void seeSouthWall() {
        if (state == State.PrimedForMainWallDeletion) {
            foreach (var obj in toDisableAfterSouth) {
                obj.SetActive(false);
            }

            state = State.WaitingForEndText;
            startTime = Time.time;
        }
    }

    void Update() {
        if (state == State.WaitingForEndText && (Time.time - startTime) >= END_TEXT_WAIT_DURATION) {
            endText.SetActive(true);
            state = State.Done;
        }
    }
}
