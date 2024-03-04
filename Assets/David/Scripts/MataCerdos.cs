using System.Collections.Generic;
using Mario.Scripts;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace David.Scripts
{
    public class MataCerdos : MonoBehaviour
    {
        public List<Animator> Porcos;
        private string trigger = "Salir";
        public float totalTime = 30.0f;
        public float elapsedTime = 0.0f;
        public float initialWaitTime = 1.5f;
        public float finalWaitTime = 0.5f;
        private float currentWaitTime;
        private float timer;

        public int score = 0;
        public TextMeshProUGUI scoreText;

        private bool isGameActive = false;

        public void AddScore()
        {
            score++;
        
            if (score <= 5) return;
        
            scoreText.text = "You win!";
            isGameActive = false;
            GlobalTimer.instance.SetLevelCompletion(3);
        }

        private void Start()
        {
            currentWaitTime = initialWaitTime;
            timer = currentWaitTime;
        }

        public void StartGame()
        {
            score = 0;
            elapsedTime = 0;
            isGameActive = true;
        }

        private void Update()
        {
            if (!isGameActive)
                return;

            elapsedTime += Time.deltaTime;
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (Porcos.Count > 0)
                {
                    int randomIndex = Random.Range(0, Porcos.Count);
                    Animator randomPorco = Porcos[randomIndex];
                    randomPorco.SetTrigger(trigger);
                }

                currentWaitTime = Mathf.Lerp(initialWaitTime, finalWaitTime, elapsedTime / totalTime);
                timer = currentWaitTime;
            }

            scoreText.text = "Time: " + (totalTime - elapsedTime) + "\n" + "Score: " + score;

            if (elapsedTime >= totalTime)
            {
                isGameActive = false;
            }
        }
    }
}
