using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    public int pointsAmount;
    public Score tracker;

    //En caso de hacer trigger añadir el score correspondiente
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            tracker.addScore(pointsAmount);
        }
    }
}
