using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFloor : MonoBehaviour
{
    public GameObject m4;
    public void ActivateM4()
    {
        WeaponSwitching.isM4Available = true;
        Debug.Log("activo2");
        m4.SetActive(false);
    }
}
