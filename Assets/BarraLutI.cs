using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraLutI : MonoBehaviour {

    public float nivel = 100.0f;
    public Image BarraFrente;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (nivel > 0)
        {
            nivel -= Time.deltaTime;
            BarraFrente.fillAmount = nivel / 100;
        }
        else
        {
            nivel = 0;
        }
    }
}
