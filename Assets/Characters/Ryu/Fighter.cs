using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {

	public Animator Animacao;
	public float velx = 0.1f;
	public float vely = 0.3f;

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

        if (Input.GetAxisRaw("Fire1") > 0)
        {
            Animacao.SetBool("ryu-mp", true);
        }
        else
            Animacao.SetBool("ryu-mp", false);

        if (Input.GetAxisRaw("Fire2") > 0)
        {
            Animacao.SetBool("ryu-lp", true);
        }
        else
            Animacao.SetBool("ryu-lp", false);

        if (Input.GetAxisRaw("Fire3") > 0)
        {
            Animacao.SetBool("ryu-guard", true);
        }
        else
            Animacao.SetBool("ryu-guard", false);

        if (Input.GetAxisRaw("Fire4") > 0)
        {
            Animacao.SetBool("ryu-hitA", true);
        }
        else
            Animacao.SetBool("ryu-hitA", false);

        if (Input.GetAxisRaw("Fire5") > 0)
        {
            Animacao.SetBool("ryu-hitB", true);
        }
        else
            Animacao.SetBool("ryu-hitB", false);


        if (Input.GetAxisRaw("Fire6") > 0)
        {
            Animacao.SetBool("ryu-win1", true);
        }
        else
            Animacao.SetBool("ryu-win1", false);

    }
}
