using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matinta     : MonoBehaviour {

    public Animator Animacao;

    // -----------------  quebra galho -------------------
    public float nivel_vida          =    100.0f; // corresponde aos 100% - deveria ser obtida de fonte externa
    public float nivel_vida_oponente =    100.0f; // corresponde aos 100% - deveria ser obtida de fonte externa
    public float distancia_openente  = 100000.0f; // deveria ser obtida de fonte externa: algo como distancia entre Pivots
    public float distancia_parede    =  30000.0f; // deveria ser obtida de fonte externa: algo como distancia entre Pivots
    public  float tempo = 0.0f;   // para gerar numero crescente para a simulação das variáveis acima
    public int num_ref = 0;
    public float updateInterval = 10.0F;
    private double lastInterval;
    // -----------------  quebra galho -------------------

    const float limite_distancia_oponente = 500.0f;
    const float limite_distancia_parede   = 100.0f;
    public int opcaolist = 0;
    public float velx = 0.1f;
    public float vely = 0.3f;

    //Declaração da lista
    public List<string> listAcoes = new List<string>();

    // Use this for initialization
    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        Animacao = GetComponent<Animator>();
        //Carrega a lista com as as ações/animações padrões para os lutadores
        listAcoes.Add("Matinta_Caminhada") ; // opcao = 0 - avançar     
        listAcoes.Add("Matinta_Atac_Soco") ; // opcao = 1 - ataque fraco
        listAcoes.Add("Matinta_Atac_Chute"); // opcao = 2 - ataque medio
        listAcoes.Add("Matinta_Atac_Espc") ; // opcao = 3 - ataque forte
        listAcoes.Add("Matinta_Vitoria")   ; // opcao = 4 - VENCE A LUTA  
        listAcoes.Add("Matinta_Caminhada") ; // opcao = 5 - recuar      
        listAcoes.Add("Matinta_Defesa")    ; // opcao = 6 - defender    
        listAcoes.Add("Matinta_Sofre_Dano"); // opcao = 7 - sofre ataque e dano
        listAcoes.Add("Matinta_KO")        ; // opcao = 8 - sofre KO

    }
    // Update is called once per frame
    void Update()
    {
        tempo += Time.time;
        float timeNow = Time.realtimeSinceStartup;
        // Promove um atraso para movimentar o lutador
        if (timeNow > lastInterval + updateInterval)
        {
            Movimenta_lutador();
            lastInterval = timeNow;
        }
    }

    // Tratamento de regras
    private void Movimenta_lutador()
    {
        Animacao.SetBool(listAcoes[opcaolist], false); // Desativa movimento anterior para ativar o novo

        // Calcula situações. Simula receber dados do oponente
        nivel_vida -= tempo / 10000;           // decrementa vida, simulando ataques
        if (nivel_vida < 0.0f) nivel_vida = 0.0f;

        nivel_vida_oponente -= tempo / 10000;  // decrementa vida, simulando ataques
        if (nivel_vida_oponente < 0.0f) nivel_vida_oponente = 0.0f;

        distancia_openente -= tempo / 1000;   // decrementa distancia, simulando ataques
        if (distancia_openente < 0.0f) distancia_openente = 0.0f;

        distancia_parede -= tempo / 100;     // decrementa distancia, simulando ataques
        if (distancia_parede < 0.0f) distancia_parede = 0.0f;

        // Calcula nova opção aleatoriamente
        num_ref = Random.Range(00, 04);
        opcaolist = num_ref;

        // verifica situação em relação ao oponente
        if (distancia_openente < limite_distancia_oponente) // oponente próximo ao lutador
        {
            if (nivel_vida < 25) // lutador tá fraquinho
            {
                opcaolist = 5; // recuar
            }
            else
            {
                opcaolist = 6; // defender
            }
            if (distancia_parede < limite_distancia_parede)  // lutador encurralado na parede
            {
                opcaolist = 3; // ataque forte
            }
        }

        if (nivel_vida < 01.0f) // sofre ataque, dano e KO
        {
            opcaolist = 7;
            Animacao.SetBool(listAcoes[opcaolist], true);  // Primeiro anima o dano
            opcaolist = 8;                                 // Depois aciona o KO
        }
        else if (nivel_vida_oponente < 01.0f)
        {
            opcaolist = 4;                            // VENCE A LUTA  		
        }

        // Executa a ação
        Animacao.SetBool(listAcoes[opcaolist], true);

        // Verifica se é avanço ou recuo do lutador
        if (opcaolist == 0)
        {
            transform.Translate(velx*4, 0f, 0f);
        }
        else if (opcaolist == 5)
        {
            transform.Translate(-velx*2, 0f, 0f);
        }
    }

    // Tratamento de colisões
/*
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BolaFogo")
        {
            opcaolist = 7;

            Animacao.SetBool(listAcoes[opcaolist], true);  // Primeiro anima o dano
            opcaolist = 8;                                 // Depois aciona o KO
            Animacao.SetBool(listAcoes[opcaolist], true);  // Primeiro anima o dano
            //Instantiate(BarraLutII).GetComponent<>().velx = -5.0f;
        }

    }
    private void OnCollisionEnter2D(Collision2D colisor)
    {
        Vector2 normal = colisor.contacts[0].normal;
    }
*/

}