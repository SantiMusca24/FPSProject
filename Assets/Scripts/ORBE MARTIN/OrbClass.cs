using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbClass : MonoBehaviour
{

    public GameObject orbObj;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        // ACA ESCRIB� LO QUE HAGA EL ORBE CUANDO LE DISPAR�S
        Debug.Log("ORB INTERACT");
        Destroy(orbObj); // esto hace que el orbe se destruya al final
    }
}
