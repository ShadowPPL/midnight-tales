using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {

	public GameObject startText;
	float timer;
	bool loadingLevel;
	bool init;

	public int activeElement;
	public GameObject menuObj;
	public ButtonRef[] menuOptions;

	// Use this for initialization
	void Start () {
		menuObj.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (!init) {
			// it flickers start
			timer += Time.deltaTime;
			if (timer > 0.6f) {
				timer = 0;
				startText.SetActive (!startText.activeInHierarchy);
			}
		

			if (Input.GetKeyUp (KeyCode.Space)) {
				init = true;
				startText.SetActive (false);
				menuObj.SetActive (true);
			}

			if (Input.GetKeyUp (KeyCode.Escape)) {
				init = false;
				startText.SetActive (true);
				menuObj.SetActive (false);
			}
		} else {
			if (!loadingLevel) {
				menuOptions [activeElement].selected = true;

				if (Input.GetKeyUp (KeyCode.UpArrow)) {
					menuOptions [activeElement].selected = false;
					if (activeElement > 0) {
						activeElement--;
					} else {
						activeElement = menuOptions.Length - 1;
					}
				}

				if (Input.GetKeyUp (KeyCode.DownArrow)) {
					menuOptions [activeElement].selected = false;
					if (activeElement < menuOptions.Length - 1) {
						activeElement++;
					} else {
						activeElement = 0;
					}
				}

			}
		}

	}
}
