using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaFloor : MonoBehaviour, IInteractuable
{
    public GameObject m4;
    public void Interact()
    {
        WeaponSwitching.isBazookaAvailable = true;
        Debug.Log("activo2");
        m4.SetActive(false);
    }
}
