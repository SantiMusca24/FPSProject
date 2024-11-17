using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour, IInteractuable
{
    // Start is called before the first frame update
    public GameObject doorKey;
    public GameObject ColiderDoor;
    public void Interact()
    {
        ColiderDoor.gameObject.SetActive(true);
        Destroy(doorKey);
    }
}
