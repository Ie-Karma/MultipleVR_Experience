using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshPro scoreText;
    private int score;


    //Gestión del score
    public void addScore(int scorePoints)
    {
        score += scorePoints;
        scoreText.text = String.Format("{0:000}", score);
    }
}
