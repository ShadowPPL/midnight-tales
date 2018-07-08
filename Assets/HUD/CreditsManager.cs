using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {

	public GameObject sectionBlock;
	public GameObject textBlock;
	public GameObject bgImage;
	public TextAsset creditData;
	public Vector2 startPosition;
	public int scrollSpeed = 2;
	public float textDistance;

	public GameObject screenFader;

	private GameObject[] _credits;
	private Vector2 _finalPos;
	private int _killScene;
	private string _nextScene;
	
	// Use this for initialization
	void Awake () {
        // Disable screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
		_killScene = 0;
		_nextScene = "TitleScreen";
		Camera.main.transform.position = new Vector3(0f,5.5f,-10f);
		bgImage.transform.position = new Vector3(0f,5.5f,1f);
		LoadCredits();
		_finalPos = _credits[_credits.Length-1].transform.position;
	}

	void LoadCredits(){
		string[] data = {};
		data = creditData.text.Split("\n"[0]);
		_credits = new GameObject[data.Length];

		for (int i = 0; i < data.Length; i++){

			string[] temp = { };
			temp = data[i].Split('|');
			if(temp.Length>1){
				_credits[i] = GameObject.Instantiate(sectionBlock) as GameObject;
				_credits[i].GetComponent<TextMesh>().text = temp[0];
			} else {
				_credits[i] = GameObject.Instantiate(textBlock) as GameObject;
				_credits[i].GetComponent<TextMesh>().text = data[i];
			}
			_credits[i].transform.position = new Vector3(startPosition.x,startPosition.y-textDistance*i,0);
			_credits[i].transform.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonUp("Fire1")){
			_killScene = 1;
		} 
		
		if (Camera.main.transform.position.y > _finalPos.y) {
			Camera.main.transform.Translate(Vector3.down * Time.deltaTime * scrollSpeed);
			bgImage.transform.Translate(Vector3.down * Time.deltaTime * scrollSpeed);
		} else
			_killScene = 3;

		if(_killScene>0) {
			StartCoroutine(NextScene());
		}
	}

	IEnumerator	NextScene ()
	{
		yield return new WaitForSeconds (2);
		float fadeTime = screenFader.GetComponent<ScreenFader>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene(_nextScene);
	}

}
