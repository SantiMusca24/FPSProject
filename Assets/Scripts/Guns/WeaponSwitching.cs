using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    [SerializeField] private GameObject _akUI, _pistolUI;

    static public int selectedWeapon = 0;
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeapon >= transform.childCount - 1) 
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount -1 ;
            }
            else
            {
                selectedWeapon--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2)&& transform.childCount >=2)
        {
            selectedWeapon = 1;

        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        if (selectedWeapon == 0)
        {
            _akUI.SetActive(true);
            _pistolUI.SetActive(false);
        }
        else if (selectedWeapon == 1)
        {
            _akUI.SetActive(false);
            _pistolUI.SetActive(true);
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon) 
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++; 
        }
    }
}
