using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	public GameObject startText;
	float timer;
	bool loadingLevel;
	bool init;

	public int activeElement;
	public GameObject menuObj;
	public ButtonRef[] menuOptions;
	public AudioClip[] sfxSound;
	public float vol;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		menuObj.SetActive (false);
	}

	void Awake () {
		source = GetComponent<AudioSource>();
		vol = 1.0f;
	}

	// Update is called once per frame
	void Update () {

		if (!init) {
			// flick start
			timer += Time.deltaTime;
			if (timer > 0.6f) {
				timer = 0;
				startText.SetActive (!startText.activeInHierarchy);
			}
		
			// enable menu
			if (Input.GetKeyUp (KeyCode.Space)) {
				init = true;
				startText.SetActive (false);
				menuObj.SetActive (true);
				source.PlayOneShot(sfxSound[1],vol);
			}
				
		} else {

			//  exit menu
			if (Input.GetKeyUp (KeyCode.Escape)) {
				init = false;
				startText.SetActive (true);
				menuObj.SetActive (false);
				source.PlayOneShot(sfxSound[2],vol);
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
					source.PlayOneShot(sfxSound[0],vol);
				}

				if (Input.GetKeyUp (KeyCode.DownArrow)) {
					menuOptions [activeElement].selected = false;
					if (activeElement < menuOptions.Length - 1) {
						activeElement++;
					} else {
						activeElement = 0;
					}
					source.PlayOneShot(sfxSound[0],vol);
				}

				if (Input.GetKeyUp (KeyCode.Space)) {
					source.PlayOneShot(sfxSound[1],vol);
					Debug.Log ("Loading Level");
					loadingLevel = true;
					StartCoroutine ("LoadLevel");
					menuOptions [activeElement].transform.localScale *= 1.2f;
				}

			}
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
		default:
			break;
		}
	}

	IEnumerator LoadLevel(){
		HandleSelectedOption();
		yield return new WaitForSeconds (0.6f);
		SceneManager.LoadSceneAsync ("CharacterSelect",LoadSceneMode.Single);
	}
}
