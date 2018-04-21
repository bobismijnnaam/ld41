using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storyline : MonoBehaviour {

    public AudioClip notificationSound;
    public GameObject frame1;
    public Text infoText;

    bool sawSouthWall;

	// Use this for initialization
	void Start () {
	    EventManager.StartListening(EventManager.SEE_NORTH_WALL, seeNorthWall);	
	    EventManager.StartListening(EventManager.SEE_SOUTH_WALL, seeSouthWall);	
	    EventManager.StartListening(EventManager.SEE_EAST_WALL, seeEastWall);	
	    EventManager.StartListening(EventManager.SEE_WEST_WALL, seeWestWall);	
        EventManager.StartListening(EventManager.SEE_PICTURE, seePicture);
        EventManager.StartListening(EventManager.SEE_NOTHING, seeNothing);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
	}

    void Update() {
    }
	
    void seeNorthWall() {
        //Debug.Log("Seeing the north wall!");
    }

    void seeEastWall() {
        //Debug.Log("Seeing the east wall!");
    }
    
    void seeWestWall() {
        //Debug.Log("Seeing the west wall!");
    }

    void seeSouthWall() {
        //Debug.Log("Seeing the south wall!");

        if (!sawSouthWall) {
            sawSouthWall = true;
            AudioSource.PlayClipAtPoint(notificationSound, new Vector3(0, 0, 0));
            spawnFirstPicture();
        }

    }

    void seePicture() {
        infoText.gameObject.SetActive(true);
    }

    void seeNothing() {
        infoText.gameObject.SetActive(false);
    }

    void spawnFirstPicture() {
        frame1.SetActive(true);
    }
}
