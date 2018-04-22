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
    public GameObject picture1Tagline;

    public GameObject frame2;

    public float picture1CameraDist;

    bool sawSouthWall;
    bool magnifying;
    bool sawLittleMan;

    Blackouter blackouter;

	// Use this for initialization
	void Start () {
	    EventManager.StartListening(EventManager.SEE_NORTH_WALL, seeNorthWall);	
	    EventManager.StartListening(EventManager.SEE_SOUTH_WALL, seeSouthWall);	
	    EventManager.StartListening(EventManager.SEE_EAST_WALL, seeEastWall);	
	    EventManager.StartListening(EventManager.SEE_WEST_WALL, seeWestWall);	
        EventManager.StartListening(EventManager.ENTER_OBSERVE_MODE, enterObserveMode);
        EventManager.StartListening(EventManager.LEAVE_OBSERVE_MODE, leaveObserveMode);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        blackouter = frame1.GetComponentsInChildren<Blackouter>()[0];
	}

    void enterObserveMode() {
        Cursor.lockState = CursorLockMode.None;
        infoText.text = "Press Q to return";
    }

    void leaveObserveMode() {
        Cursor.lockState = CursorLockMode.Locked;
        infoText.text = "Press E to examine";
    }

    void Update() {
        if (!picture1Camera.gameObject.activeSelf) {
            if (isLookingAtPicture1()) {
                Debug.Log("Trying to send keep info alive...");
                EventManager.TriggerEvent(EventManager.KEEP_INFO_ALIVE);
            }

            if (isLookingAtPicture1() && Input.GetKeyDown(KeyCode.E)) {
                fpsCamera.gameObject.SetActive(false);
                picture1Camera.gameObject.SetActive(true);
                magnifyingCamera.gameObject.SetActive(true);

                infoText.text = "Press Q to return";

                Cursor.lockState = CursorLockMode.None;
                rectangleBorder.gameObject.SetActive(true);

                magnifying = true;

                EventManager.TriggerEvent(EventManager.ENTER_OBSERVE_MODE);

                blackouter.areBeingObserved();
            }
        }


        if (magnifying) {
            EventManager.TriggerEvent(EventManager.KEEP_INFO_ALIVE);

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

            if (rch.collider.name == "LittleMan" && !sawLittleMan) {
                sawLittleMan = true;
                spawnSecondPicture();
            }

            rectangleBorder.transform.position = new Vector3(mousePos.x, mousePos.y, 1);
            rectangleBorder.rectTransform.sizeDelta = viewportSize;
        }

        if (picture1Camera.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Q)) {
            EventManager.TriggerEvent(EventManager.LEAVE_OBSERVE_MODE);
            magnifying = false;

            fpsCamera.gameObject.SetActive(true);
            picture1Camera.gameObject.SetActive(false);
            magnifyingCamera.gameObject.SetActive(false);
            infoText.text = "Press E to examine";

            Cursor.lockState = CursorLockMode.Locked;

            rectangleBorder.gameObject.SetActive(false);

            if (sawLittleMan) {
                var obj = GameObject.Find("LittleMan");
                if (obj != null) {
                    GameObject.Destroy(obj);
                    var tm = picture1Tagline.GetComponent<TextMesh>();
                    tm.text = "Fields";
                }
            }
        }
    }

    void seeNorthWall() {
    }

    void seeEastWall() {
    }
    
    void seeWestWall() {
    }

    void seeSouthWall() {
        if (!sawSouthWall) {
            sawSouthWall = true;
            spawnFirstPicture();
        }

    }

    void spawnFirstPicture() {
        ringBell(frame1.transform.position);
        frame1.SetActive(true);
    }

    void spawnSecondPicture() {
        ringBell(new Vector3(0, 0, 0));
        frame2.SetActive(true);
    }

    void ringBell(Vector3 pos) {
        AudioSource.PlayClipAtPoint(notificationSound, pos);
    }

    bool isLookingAtPicture1() {
        var start = fpsCamera.transform.position;
        var dir = fpsCamera.transform.forward;
        
        RaycastHit rch;
        if (Physics.Raycast(start, dir, out rch, 3)) {
            return rch.collider.gameObject.name == "Picture1";
        }

        return false;
    }
}
