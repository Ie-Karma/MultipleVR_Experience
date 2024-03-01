using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MataCerdos : MonoBehaviour
{
    //lista de posiciones de cerdos
    public List<Animator> Porcos;

    private string trigger = "Salir";
    
    public float totalTime = 30.0f;
    public float elapsedTime = 0.0f;
    public float initialWaitTime = 1.5f;
    public float finalWaitTime = 0.5f;
    private bool isCoroutineActive = false;

    public int score = 0;
    
    public TextMeshProUGUI scoreText;
    
    public void AddScore()
    {
        score++;
    }

    private void Update()
    {
        if (isCoroutineActive)
        {
            elapsedTime += Time.deltaTime;
        }

        scoreText.text = "Time: " + (totalTime - elapsedTime) + "\n" + "Score: " + score;
    }

    public void StartGame()
    {
        score = 0;
        elapsedTime = 0;
        isCoroutineActive = true;
        StartCoroutine(ActivateRandomPorcoWithDecreasingInterval());
    }
    
    IEnumerator ActivateRandomPorcoWithDecreasingInterval()
    {
        while (elapsedTime < totalTime)
        {
            float waitTime = Mathf.Lerp(initialWaitTime, finalWaitTime, elapsedTime / totalTime);
            yield return new WaitForSeconds(waitTime);

            if (Porcos.Count > 0)
            {
                int randomIndex = Random.Range(0, Porcos.Count);
                Animator randomPorco = Porcos[randomIndex];
                randomPorco.SetTrigger(trigger);
            }
        }

        isCoroutineActive = false;
    }
}
