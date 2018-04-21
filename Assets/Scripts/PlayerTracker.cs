using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        var y = transform.eulerAngles.y;
        if (y > 355 || y < 5) {
            EventManager.TriggerEvent(EventManager.SEE_NORTH_WALL);
        } else if (y > 265 && y < 275) {
            EventManager.TriggerEvent(EventManager.SEE_WEST_WALL);
        } else if (y > 175 && y < 185) {
            EventManager.TriggerEvent(EventManager.SEE_SOUTH_WALL);
        } else if (y > 85 && y < 95) {
            EventManager.TriggerEvent(EventManager.SEE_EAST_WALL);
        }
	}
}
