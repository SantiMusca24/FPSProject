using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaBullet : MonoBehaviour
{
    [SerializeField] static public int _currentBullets; // BALAS ACTUALES
    [SerializeField] static public int _maxBullets = 1; // BALAS MÁXIMAS
    [SerializeField]
    private GameObject _bullet;// ÍCONOS DE BALAS 
    [SerializeField] static public bool _shootCooldown = false; // EL JUGADOR NO PODRÁ DISPARAR CUANDO EL COOLDOWN ESTÉ ACTIVO
    [SerializeField] static public bool _canShoot = true;
    //[SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        // EL JUGADOR EMPIEZA CON BALAS MÁXIMAS
        _currentBullets = _maxBullets;
        _shootCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (_currentBullets <= 0 ) _canShoot = false;
        else _canShoot = true;

        if (Input.GetMouseButtonDown(0) && WeaponSwitching.selectedWeapon == 2 && _canShoot && !_shootCooldown)
        {
            _shootCooldown = true;
            StartCoroutine(shottyCooldown());
        }


        // CADA NÚMERO DE BALAS DEFINE CUÁLES ÍCONOS ESTÁN ACTIVOS
        if (_currentBullets == 1)
        {

            _bullet.SetActive(true);

        }
        else if (_currentBullets == 0)
        {
            _bullet.SetActive(false);
        }

        IEnumerator shottyCooldown()
        {
            yield return new WaitForSeconds(0.3f);
            _shootCooldown = false;
        }

    }



}
