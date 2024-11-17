using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFloor : MonoBehaviour, IInteractuable
{
    public GameObject m4;
    public void Interact()
    {
        WeaponSwitching.isM4Available = true;
        Debug.Log("activo2");
        m4.SetActive(false);
    }
}
