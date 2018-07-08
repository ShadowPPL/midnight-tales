using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {
	
	public Texture2D fadeTexture;
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	private int fadeDirection = -1;			// The direction to fade: in = -1 or out = 1
	private float alpha = 1.0f;				// Texture alpha

	void OnEnable()
	{
		//Tell our 'FadeAway' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += FadeIn;
		Debug.Log ("'FadeIn' loaded");
	}

	void OnDisable()
	{
		//Tell our 'FadeAway' function to stop listening for a scene change as soon as this script is disabled.
		//Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= FadeIn;
		Debug.Log ("'FadeIn' UNloaded");
	}
	
	void OnGUI ()
	{
		alpha += fadeDirection * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01(alpha);
		GUI.color = new Color(GUI.color.r,GUI.color.g,GUI.color.b,alpha);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
 	}

 	public float BeginFade(int direction)
 	{
 		fadeDirection = direction;
 		return (fadeSpeed);
 	}

	void FadeIn(Scene scene, LoadSceneMode mode)
 	{
 		BeginFade(-1);
 	}

}