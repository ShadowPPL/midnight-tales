using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour {

    public float tempo = 100.0f;
    public Text TimeRound;
    public Image img;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (tempo > 0)
        {
            tempo -= Time.deltaTime;
        }
        else
        {
            tempo = 0;
        }
        TimeRound.text = tempo.ToString("0");

    }
}
