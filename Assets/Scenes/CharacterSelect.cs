using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {

	public int numberOfPlayers = 1;
	public List<PlayerInterfaces> plInterfaces = new List<PlayerInterfaces> ();
	// all entries portraits
	public PortraitInfo[] portraitPrefabs;
	//hardcoded max portraits
	public int maxX;
	public int maxY;
	// grid for select entries
	PortraitInfo[,] charGrid;
	// canvas containing all portraits
	public GameObject portraitCanvas;
	bool loadLevel;
	public bool bothPlayersSelected;
	public AudioClip[] sfxSound;
	public float vol;
	CharacterManager charManager;
	AudioSource audioSource;

	#region Singleton
	public static CharacterSelect instance;

	public static CharacterSelect GetInstance(){
		return instance;
	}

	void Awake () {
		instance = this;
		audioSource = GetComponent<AudioSource>();
		vol = 1.0f;
	}
	#endregion

	// Use this for initialization
	void Start () {
		charManager = CharacterManager.GetInstance ();
		numberOfPlayers = charManager.numberOfUsers;

		//create the grid
		charGrid = new PortraitInfo[maxX, maxY];

		int x = 0;
		int y = 0;

		portraitPrefabs = portraitCanvas.GetComponentsInChildren<PortraitInfo> ();

		// go into each portrait and
		for (int i = 0; i < portraitPrefabs.Length; i++) {
			//assign a position
			portraitPrefabs [i].posX += x;
			portraitPrefabs [i].posY += y;

			charGrid [x, y] = portraitPrefabs [i];

			if (x < maxX-1) {
				x++;
			} else {
				x = 0;
				y++;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (!loadLevel) {
			for (int i = 0; i < plInterfaces.Count; i++) {
				if (i < numberOfPlayers) {

					// if user has pressed, he has unselected a char
//					if (Input.GetButtonUp ("Fire2" + charManager.players[i].inputId)) {
//						plInterfaces[i].playerBase.hasCharacter = false;
//					}

					if (!charManager.players[i].hasCharacter) {
						plInterfaces[i].playerBase = charManager.players[i];
						HandleSelectorPosition (plInterfaces[i]);
						HandleSelectScreenInput (plInterfaces[i], charManager.players[i].inputId);
						HandleCharacterPreview (plInterfaces[i]);
					}
				} else {
					charManager.players[i].hasCharacter = true;
				}
				
			}
		}

		if (bothPlayersSelected) {
			Debug.Log ("Loading...");
			StartCoroutine ("LoadLevel");
			loadLevel = true;
		} else {
			if (charManager.players[0].hasCharacter	&& charManager.players[1].hasCharacter) {
				bothPlayersSelected = true;
			}
		}
	}

	IEnumerator LoadLevel(){
		// if one of players is AI, assign a random char to prefab
		for (int i = 0; i < charManager.players.Count; i++) {
			if (charManager.players[i].playerType == PlayerBase.PlayerType.ai) {
				if (charManager.players[i].playerPrefab == null) {
					int randVal = Random.Range (0, portraitPrefabs.Length);

					charManager.players [i].playerPrefab = 
						charManager.GetCharByID(portraitPrefabs[randVal].characterId).prefab;

					Debug.Log(portraitPrefabs[randVal].characterId);
				}
			}
		}
		yield return new WaitForSeconds (2);
		float fadeTime = GameObject.Find ("ScreenFader").GetComponent<ScreenFader>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadSceneAsync ("Stage01",LoadSceneMode.Single);
	}

	void HandleSelectorPosition(PlayerInterfaces pl){
		// enable selector
		pl.selector.SetActive (true);
		//find active portrait
		pl.activePortrait = charGrid [pl.activeX, pl.activeY];
		//and places cursor over it
		Vector2 selectorPosition = pl.activePortrait.transform.localPosition;
		selectorPosition = selectorPosition + new Vector2 (portraitCanvas.transform.localPosition.x, 
			portraitCanvas.transform.localPosition.y);
		pl.selector.transform.localPosition = selectorPosition;
	}

	void HandleSelectScreenInput(PlayerInterfaces pl, string playerId){
		#region Grid Navigation
		float vertical = Input.GetAxis ("Vertical" + playerId);
		if (vertical != 0) {
			if (!pl.hitInputOnce) {
				if (vertical > 0) {
					pl.activeY = (pl.activeY > 0) ? pl.activeY - 1 : maxY - 1;
					audioSource.PlayOneShot(sfxSound[0],vol);
				} else {
					pl.activeY = (pl.activeY < maxY - 1) ? pl.activeY + 1 : 0;
					audioSource.PlayOneShot(sfxSound[0],vol);
				}
				pl.hitInputOnce = true;
			}
		}

		float horizontal = Input.GetAxis ("Horizontal" + playerId);
		if (horizontal != 0) {
			if (!pl.hitInputOnce) {
				if (horizontal > 0) {
					pl.activeX = (pl.activeX > 0) ? pl.activeX - 1 : maxX - 1;
					audioSource.PlayOneShot(sfxSound[0],vol);
				} else {
					pl.activeX = (pl.activeX < maxX - 1) ? pl.activeX + 1 : 0;
					audioSource.PlayOneShot(sfxSound[0],vol);
				}
				pl.timerToReset = 0;
				pl.hitInputOnce = true;
			}
		}

		if (vertical == 0 && horizontal == 0) {
			pl.hitInputOnce = false;
		}

		if (pl.hitInputOnce) {
			pl.timerToReset += Time.deltaTime;
			if (pl.timerToReset > 0.8f) {
				pl.hitInputOnce = false;
				pl.timerToReset = 0;
			}
		}
		#endregion
		// if user has pressed space, he has selected a char
		if (Input.GetButtonUp ("Fire1" + playerId)) {
			// make a reaction on char
		//	pl.createdCharacter.GetComponentInChildren<Animator> ().Play ("Kick");
			// pass character to character manager
			audioSource.PlayOneShot(sfxSound[1],vol);
			pl.playerBase.playerPrefab = charManager.GetCharByID (pl.activePortrait.characterId).prefab;
			pl.playerBase.hasCharacter = true;
		}
			
	}

	void HandleCharacterPreview(PlayerInterfaces pl){
		if (pl.previewPortrait != pl.activePortrait) {		// if previous port isnt the same of active = we changed chars, so
			if (pl.createdCharacter != null) { 				// if we have one char
				Destroy (pl.createdCharacter); 				//destroy it
			}

			//and create another one
			GameObject go = Instantiate (
                CharacterManager.GetInstance().GetCharByID(pl.activePortrait.characterId).prefab,
                pl.charVisPos.position,
                Quaternion.identity) as GameObject;
			
			pl.createdCharacter = go;
			pl.previewPortrait = pl.activePortrait;

			if (!string.Equals (pl.playerBase.playerId,charManager.players[0].playerId)) {
//				pl.createdCharacter.GetComponent<StateManager> ().lookRight = false;
			}
		}	
	}

	[System.Serializable]
	public class PlayerInterfaces
	{
		public PortraitInfo activePortrait;		// current active portrait for P1
		public PortraitInfo previewPortrait;
		public Transform charVisPos;			// visualization position for P1
		public GameObject selector;				// selection cursor for P1
		public GameObject createdCharacter;		// created char for P1

		// active X Y entries for Player
		public int activeX;
		public int activeY;

		// smooth out input
		public bool hitInputOnce;
		public float timerToReset;

		public PlayerBase playerBase;
	}
}
