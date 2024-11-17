using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBox : MonoBehaviour, IInteractuable
{
    public void Interact()
    {
        if (WeaponSwitching.selectedWeapon == 0) GunController._akMunnition = 3;
        else if (WeaponSwitching.selectedWeapon == 1) GunController._pistolMunnition = 3;
    }

}
