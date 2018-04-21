using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storyline : MonoBehaviour {

    public AudioClip notificationSound;
    public GameObject frame1;
    public Text infoText;
    public Image rectangleBorder;
    public Camera fpsCamera;
    public Camera picture1Camera;
    public Camera magnifyingCamera;

    public float picture1CameraDist;

    bool sawSouthWall;
    bool magnifying;

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
        if (infoText.gameObject.activeSelf && Input.GetKeyDown(KeyCode.E)) {
            fpsCamera.gameObject.SetActive(false);
            picture1Camera.gameObject.SetActive(true);
            magnifyingCamera.gameObject.SetActive(true);
            infoText.text = "Press Q to return";

            Cursor.lockState = CursorLockMode.None;
            rectangleBorder.gameObject.SetActive(true);

            magnifying = true;
        }

        if (magnifying) {
            var mousePos = Input.mousePosition;
            var viewportSize = new Vector2(200, 200);
            
            var ray = picture1Camera.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 10));
            RaycastHit rch;
            Physics.Raycast(ray, out rch);

            var place = rch.point;
            place.z = place.z - picture1CameraDist;
            magnifyingCamera.gameObject.transform.position = place;
            magnifyingCamera.pixelRect = new Rect(
                    mousePos.x - viewportSize.x / 2,
                    mousePos.y - viewportSize.y / 2,
                    viewportSize.x,
                    viewportSize.y
                    );

            rectangleBorder.transform.position = new Vector3(mousePos.x, mousePos.y, 1);
            rectangleBorder.rectTransform.sizeDelta = viewportSize;
        }

        if (picture1Camera.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Q)) {
            fpsCamera.gameObject.SetActive(true);
            picture1Camera.gameObject.SetActive(false);
            magnifyingCamera.gameObject.SetActive(false);
            infoText.text = "Press E to examine";

            Cursor.lockState = CursorLockMode.Locked;

            rectangleBorder.gameObject.SetActive(false);
        }
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
