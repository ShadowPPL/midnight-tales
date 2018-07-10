using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	public GameObject startText;
	public int activeElement;
	public GameObject menuObj;
	public ButtonRef[] menuOptions;
	public AudioClip[] sfxSound;
	public float vol;

	float timer;
	bool loadingLevel;
	bool init;
	AudioSource audioSource;
	string nextScene;

	void Awake () {
		audioSource = GetComponent<AudioSource>();
		vol = 1.0f;
	}

	// Use this for initialization
	void Start () {
		menuObj.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

		if (!init) {
			// flick start
			Flick (startText, 0.6f);
		
			// enable menu
			if (Input.GetKeyUp (KeyCode.Space)) {
				init = true;
				startText.SetActive (false);
				menuObj.SetActive (true);
				audioSource.PlayOneShot(sfxSound[1],vol);
			}
				
		} else {

			//  exit menu
			if (Input.GetKeyUp (KeyCode.Escape)) {
				init = false;
				startText.SetActive (true);
				menuObj.SetActive (false);
				audioSource.PlayOneShot(sfxSound[2],vol);
			}

			// menu navigation
			if (!loadingLevel) {
				
				menuOptions [activeElement].selected = true;

				if (Input.GetKeyUp (KeyCode.UpArrow)) {
					menuOptions [activeElement].selected = false;
					if (activeElement > 0) {
						activeElement--;
					} else {
						activeElement = menuOptions.Length - 1;
					}
					audioSource.PlayOneShot(sfxSound[0],vol);
				}

				if (Input.GetKeyUp (KeyCode.DownArrow)) {
					menuOptions [activeElement].selected = false;
					if (activeElement < menuOptions.Length - 1) {
						activeElement++;
					} else {
						activeElement = 0;
					}
					audioSource.PlayOneShot(sfxSound[0],vol);
				}

				if (Input.GetKeyUp (KeyCode.Space)) {
					audioSource.PlayOneShot(sfxSound[1],vol);
					Debug.Log ("Loading Level");
					loadingLevel = true;
					StartCoroutine ("LoadLevel");
					menuOptions [activeElement].transform.localScale *= 1.2f;
				}

			}
		}

	}

	void Flick(GameObject go, float t){
		timer += Time.deltaTime;
		if (timer > t) {
			timer = 0;
			go.SetActive (!go.activeInHierarchy);
		}
	}

	// handle the selected option
	void HandleSelectedOption(){
		switch (activeElement) {
		case 0:
			CharacterManager.GetInstance ().numberOfUsers = 1;
			break;
		case 1:
			CharacterManager.GetInstance ().numberOfUsers = 2;
			CharacterManager.GetInstance ().players[1].playerType = PlayerBase.PlayerType.user;
			break;
		case 2:
			nextScene = "Credits";
			break;
		default:
			Debug.Log ("No valid option from menu");
			break;
		}
	}

	IEnumerator LoadLevel(){
		nextScene = "CharacterSelect";
		HandleSelectedOption();
		yield return new WaitForSeconds (0.6f);
		float fadeTime = GameObject.Find ("ScreenFader").GetComponent<ScreenFader>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadSceneAsync (nextScene,LoadSceneMode.Single);
	}
}
