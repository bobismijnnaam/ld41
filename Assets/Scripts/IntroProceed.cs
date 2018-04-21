using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroProceed : MonoBehaviour {

    public Text introText;
    public AudioClip introSound;
    public AudioClip pianoSound;
    public int beforeDuration;
    public int afterDuration;

    private float startTime;
    private bool hasStartedSound;

    private AsyncOperation ao;

	// Use this for initialization
	void Start () {
        introText.gameObject.SetActive(false);
		startTime = Time.time;

        ao = SceneManager.LoadSceneAsync("Room");
        ao.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update () {
        if ((Time.time - startTime) > beforeDuration && !introText.gameObject.activeSelf) {
            introText.gameObject.SetActive(true);
            AudioSource.PlayClipAtPoint(pianoSound, new Vector3(0, 1, -10));
        }

        var untilSoundStart = afterDuration - introSound.length;
        var soundStartPoint = beforeDuration + untilSoundStart;

        if ((Time.time - startTime) > soundStartPoint && !hasStartedSound) {
            AudioSource.PlayClipAtPoint(introSound, new Vector3(0, 1, -10));
            hasStartedSound = true;
        }

        if ((Time.time - startTime) > (beforeDuration + afterDuration)) {
            //SceneManager.LoadScene("Room");
            ao.allowSceneActivation = true;
        }
	}
}
