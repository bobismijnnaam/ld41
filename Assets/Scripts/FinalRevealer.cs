using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRevealer : MonoBehaviour {

    public GameObject[] toDisable;
    public GameObject[] toDisableAfterSouth;
    public GameObject[] toEnable;
    public Material starSkybox;

    int blackedOutPictureCount = 0;

    enum State {
        Idle,
        PrimedForMainWallDeletion,
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

            state = State.Done;
        }
    }
}
