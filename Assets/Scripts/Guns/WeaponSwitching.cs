using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    [SerializeField] private GameObject _akUI, _pistolUI;
    [SerializeField] private Animator _akReload, _pistolShoot;


    static public int selectedWeapon = 0;
    void Start()
    {
        _akReload.keepAnimatorStateOnDisable = true;
        _pistolShoot.keepAnimatorStateOnDisable = true;


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
                //_akReload.Play("normalState", 0, 0f);
                //_pistolShoot.Play("M1911@Fire", 0, 0f);
                selectedWeapon = 0;
                BulletCounter._shootCooldown = false;
                PistolBullets._shootCooldown = false;
            }
            else
            {
                _akReload.Play("Reload_m4", 0, 0f);
                //_pistolShoot.Play("normalState", 0, 0f);
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
                //_pistolShoot.Play("normalState", 0, 0f);
                selectedWeapon = transform.childCount -1 ;
                BulletCounter._shootCooldown = false;
                PistolBullets._shootCooldown = false;

            }
            else
            {
                //_akReload.Play("normalState", 0, 0f);
                //_pistolShoot.Play("M1911@Fire", 0, 0f);
                selectedWeapon--;
                BulletCounter._shootCooldown = false;
                PistolBullets._shootCooldown = false;

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //_akReload.Play("normalState", 0, 0f);
            //_pistolShoot.Play("M1911@Fire", 0, 0f);
            selectedWeapon = 0;
            BulletCounter._shootCooldown = false;
            PistolBullets._shootCooldown = false;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2)&& transform.childCount >=2)
        {
            _akReload.Play("Reload_m4", 0, 0f);
            //_pistolShoot.Play("normalState", 0, 0f);
            selectedWeapon = 1;
            BulletCounter._shootCooldown = false;
            PistolBullets._shootCooldown = false;

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
