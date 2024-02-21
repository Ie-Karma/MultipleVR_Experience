using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    //Obtener game manager
    public GameManagerLaberinto gameManager;


    //Inicializar el game manager
    void Start()
    {
        gameManager = GameManagerLaberinto.Instance;
    }


    public void Update()
    {
        //Lanzar audio aleatorio con un 0.00001% de probabilidad
        if (Random.value < 0.001)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

    //El objeto es grab interactable, si se hace grab, se lanza un audio y se destruye el objeto
    public void OnGrab()
    {
        //Aumentar contador de monedas
        gameManager.coins++;

        Debug.Log("Coin grabbed");
        AudioSource audio = GetComponent<AudioSource>();
        //Cambiar clip al path "Assets/Rodrigo/audio/collectcoin-6075.mp3"
        audio.clip = Resources.Load<AudioClip>("audio/collectcoin-6075");


        audio.Play();
        Destroy(gameObject, audio.clip.length);
    }
}
