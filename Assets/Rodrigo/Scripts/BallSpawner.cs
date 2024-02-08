using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    //Objeto que se va a instanciar
    public GameObject prefab;

    //Variable que indica si ya hay un objeto
    private bool hasObject;

    private void Update()
    {   
        //Si no hay un objeto, se instancia uno
        if (!hasObject)
        {
            StartCoroutine(CreatObject());
        }
    }

    //Si el objeto sale del trigger, se cambia la variable hasObject
    void OnTriggerExit(Collider other)
    {
        hasObject &= !other.CompareTag("Ball");
    }

    //Si el objeto entra al trigger, se cambia la variable hasObject
    private void OnTriggerEnter(Collider other)
    {
        hasObject |= other.CompareTag("Ball");
    }

    //Corrutina que instancia el objeto y espera 1 segundo
    IEnumerator CreatObject()
    {
        hasObject = true;
        yield return new WaitForSeconds(1);
        Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }
}
