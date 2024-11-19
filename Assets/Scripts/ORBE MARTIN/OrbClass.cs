using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbClass : MonoBehaviour
{

    public GameObject orbObj;
    private float currentCharge;


    

    //TP2 Martin Rabanal (Getter y Setter)
    public float Charge
    {
        get { return currentCharge; }
        set
        {
            if (value>=0f)
            {
                currentCharge -= 1f;
                Debug.Log("Objeto no destruido");

            }
            else
            {
                Destroy(orbObj);
                Debug.Log("Objeto destruido");
            }
        }
    }


    public void Interact()
    {
        Charge = 5f;
        Debug.Log("funciona");
    }


    private void FixedUpdate()
    {
        if (true)
        {

        }
    }


}
