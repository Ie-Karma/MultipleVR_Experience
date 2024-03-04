using System;
using System.Collections;
using System.Collections.Generic;
using Mario.Scripts;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshPro scoreText;
    private int score;


    //Gesti�n del score
    public void addScore(int scorePoints)
    {
        score += scorePoints;
        scoreText.text = String.Format("{0:000}", score);
        
        if (score < 100) return;
        GlobalTimer.instance.SetLevelCompletion(7);
        scoreText.text = "You win!";

    }
}
