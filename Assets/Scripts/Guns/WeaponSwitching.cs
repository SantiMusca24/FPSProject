using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    [SerializeField] private GameObject _akUI, _pistolUI;
    [SerializeField] private Animator _akReload, _pistolShoot;


    static public int selectedWeapon = 1;
    static public bool isM4Available = false;
    void Start()
    {
        _akReload.keepAnimatorStateOnDisable = true;
        _pistolShoot.keepAnimatorStateOnDisable = true;

        isM4Available = false;

        SelectWeapon();
    }


    void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

        
        if (isM4Available)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                {
                    _pistolShoot.Play("Idle", 0, 0f);
                    selectedWeapon = 0; 
                    BulletCounter._shootCooldown = false;
                    PistolBullets._shootCooldown = false;
                }
                else
                {
                    _akReload.Play("Reload_m4", 0, 0f);
                    selectedWeapon++; 
                    BulletCounter._shootCooldown = false;
                    PistolBullets._shootCooldown = false;
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                {
                    _akReload.Play("Reload_m4", 0, 0f);
                    selectedWeapon = transform.childCount - 1; 
                    BulletCounter._shootCooldown = false;
                    PistolBullets._shootCooldown = false;
                }
                else
                {
                    _pistolShoot.Play("Idle", 0, 0f);
                    selectedWeapon--; // Cambiar a pistola
                    BulletCounter._shootCooldown = false;
                    PistolBullets._shootCooldown = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            {
                _akReload.Play("Reload_m4", 0, 0f);
                selectedWeapon = 1; // Cambiar a M4
                BulletCounter._shootCooldown = false;
                PistolBullets._shootCooldown = false;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _pistolShoot.Play("Idle", 0, 0f);
            selectedWeapon = 0; // Cambiar a pistola
            BulletCounter._shootCooldown = false;
            PistolBullets._shootCooldown = false;
        }


        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        if (selectedWeapon == 1)
        {
            _akUI.SetActive(false);
            _pistolUI.SetActive(true); 
        }
        else if (selectedWeapon == 0 && isM4Available)
        {
            _akUI.SetActive(true); 
            _pistolUI.SetActive(false);
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
