using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBox : MonoBehaviour, IInteractuable
{
    public void Interact()
    {
        if (WeaponSwitching.selectedWeapon == 0) GunClass._akMunnition = WeaponSwitching.machinegun.rel;
        else if (WeaponSwitching.selectedWeapon == 1) GunClass._pistolMunnition = WeaponSwitching.handgun.rel;
    }

}
