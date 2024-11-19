using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour, IInteractuable
{
    // Lorenzo Marmol
    public GameObject doorKey;
    public GameObject ColiderDoor;
    public void Interact()
    {
        ColiderDoor.gameObject.SetActive(true);
        Destroy(doorKey);
    }
}
