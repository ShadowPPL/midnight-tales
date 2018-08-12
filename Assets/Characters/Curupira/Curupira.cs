using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curupira : MonoBehaviour {

    public Animator Animacao;
    public GameObject Prefabfogo;

    public float velx = 0.1f;
    public float vely = 0.3f;

    public int contDispFogo = 0;
    public int maxDispFogo  = 1;

    //public float updateInterval = 3.0F;

    // Use this for initialization
    void Start() {
        Animacao = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetAxisRaw("Fire41") > 0)            // teclado:  T 
        {
            Animacao.SetBool("Caminhada", true);
            transform.Translate(velx, 0f, 0f);
        }
        else if (Input.GetAxisRaw("Fire41") < 0)            // teclado:  Y
        {
            Animacao.SetBool("Caminhada", true);
            transform.Translate(-velx, 0f, 0f);
        }
        else
        {
            Animacao.SetBool("Caminhada", false);
        }

        if (Input.GetAxisRaw("Fire42") > 0)            // teclado:  U
        { Animacao.SetBool("Ataque_01", true); }
        else Animacao.SetBool("Ataque_01", false);

        if (Input.GetAxisRaw("Fire43") > 0)             // teclado:  I
        { Animacao.SetBool("Ataque_02", true); }
        else
        {
            Animacao.SetBool("Ataque_02", false);
        }

        if (Input.GetAxisRaw("Fire44") > 0)             // teclado:  O
        {
            //float timeNow = Time.realtimeSinceStartup;
            Animacao.SetBool("Ataque_03", true);
            DisparaFogo ();
        }
        else
        {
            Animacao.SetBool("Ataque_03", false);
            contDispFogo = 0;
        }

        if (Input.GetAxisRaw("Fire45") > 0)             // teclado:  P
        { Animacao.SetBool("Vitoria", true); }
        else Animacao.SetBool("Vitoria", false);


        if (Input.GetAxisRaw("Fire46") > 0)             // teclado:  L
        { Animacao.SetBool("Defesa", true); }
        else Animacao.SetBool("Defesa", false);

        if (Input.GetAxisRaw("Fire47") > 0)             // teclado:  K
        { Animacao.SetBool("KO", true); }
        else
        {
            Animacao.SetBool("KO", false);
        }
    }

    private void DisparaFogo() //   DisparaFogo(float timeNow)
    {
        if (contDispFogo < maxDispFogo) // qtde de fogos disparados
        {
            //float delay = timeNow - Time.time;

            //Debug.Log(delay); // verificação de valores

            // promove um delay par esperar a animação do sptrite do curupira
            //if (delay > updateInterval)
            //{

                // Fogo do curupira - Cria nova bola de Fogo do curupira
                GameObject novaBola = Instantiate(Prefabfogo);

                // Posiciona nova bola de fog na altura do pivot do curupira
                    //novaBola.transform.position = this.transform.position;

                // Posiciona nova bola de fog na altura da mão do curupira e à frente do corpo dele
                novaBola.transform.position = new Vector3(this.transform.position.x - 2.0f, this.transform.position.y, this.transform.position.z);

                // Define o sentido da nova bola de fogo (x)

                //novaBola.GetComponent<ControladorFogo>().velx = -5.0f;
                //Instantiate(novaBola).GetComponent<Rigidbody>().AddForce(new Vector3(-1000f, 0, 0));

                novaBola.GetComponent<Rigidbody>().AddForce(new Vector3(-1000f, 0, 0));

                contDispFogo++; // para controle da qtde de bolas disparadas
            //}
        }
    }
}
