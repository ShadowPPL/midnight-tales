using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {

	public Animator Animacao;
	public float velx = 0.3f;
	public float vely = 0.5f;


	// Use this for initialization
	void Start () {
		Animacao = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw ("Horizontal") > 0) {
			Animacao.SetBool ("forward", true);
			transform.Translate(velx,0f,0f);
		} else
			Animacao.SetBool ("forward", false);

		if (Input.GetAxisRaw ("Horizontal") < 0) {
			Animacao.SetBool ("backward", true);
			transform.Translate(-velx,0f,0f);
		} else
			Animacao.SetBool ("backward", false);
		
		if (Input.GetAxisRaw ("Vertical") > 0) {
			Animacao.SetBool ("up", true);
			transform.Translate(0f,vely,0f);
		} else
			Animacao.SetBool ("up", false);
		
		if (Input.GetAxisRaw ("Vertical") < 0) {
			Animacao.SetBool ("down", true);
		} else
			Animacao.SetBool ("down", false);
	}
}
