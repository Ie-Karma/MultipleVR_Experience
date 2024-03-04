using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerLaberinto : MonoBehaviour
{
    //Singleton
    public static GameManagerLaberinto instance;
    public static GameManagerLaberinto Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManagerLaberinto();
            }
            return instance;
        }
    }   

    //Contador de monedas
    public int coins = 0;

    //Maximo de monedas
    public int maxCoins = 3;

    //Cmabio de escena a MaquinaGancho 1 si se recogen todas las monedas
    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Inicializar el singleton
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Si se recogen todas las monedas, cambiar de escena
        if (coins == maxCoins)
        {
            ChangeScene();
        }
        
    }
}
