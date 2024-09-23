using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject doorKey;
    public GameObject ColiderDoor;

    private void OnTriggerEnter(Collider Player)
    {
       ColiderDoor.gameObject.SetActive(true);
       Destroy(doorKey);
    }
}
