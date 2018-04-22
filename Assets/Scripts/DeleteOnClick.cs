using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeleteOnClick : MonoBehaviour {
    public GameObject screenSlide;
    public Material slide2;

    enum State {
        Slide1,
        Slide2
    }

    State state;

	// Use this for initialization
	void Start () {
		state = State.Slide1;
	}
		
	public void OnMouseDown() {
        switch (state) {
            case State.Slide1:
                state = State.Slide2;
                screenSlide.GetComponent<Image>().material = slide2;
                break;
            case State.Slide2:
                SceneManager.LoadScene("Intro");
                break;
        }
    }
}
