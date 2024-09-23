using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCounter : MonoBehaviour
{
    [SerializeField] static public int _currentBullets; // BALAS ACTUALES
    [SerializeField] static public int _maxBullets = 16; // BALAS MÁXIMAS
    [SerializeField] private GameObject _bullet1, _bullet2, _bullet3, _bullet4, _bullet5, _bullet6, _bullet7, 
    _bullet8, _bullet9, _bullet10, _bullet11, _bullet12, _bullet13, _bullet14, _bullet15, _bullet16;  // ÍCONOS DE BALAS 
    [SerializeField] static public bool _shootCooldown = false; // EL JUGADOR NO PODRÁ DISPARAR CUANDO EL COOLDOWN ESTÉ ACTIVO
    [SerializeField] private AudioSource _reloadNoise;
    [SerializeField] static public bool _canShoot = true;
    [SerializeField] private Animator _animator;


    // Start is called before the first frame update
    void Start()
    {
        // EL JUGADOR EMPIEZA CON BALAS MÁXIMAS
        _currentBullets = _maxBullets;
        _shootCooldown=false;
    }

    // Update is called once per frame
    void Update()
    {

        if (_currentBullets <= 0) _canShoot = false;
        else _canShoot = true;

        // CADA NÚMERO DE BALAS DEFINE CUÁLES ÍCONOS ESTÁN ACTIVOS
        if (_currentBullets == 16)
        {
            _bullet1.SetActive(true);
            _bullet2.SetActive(true);
            _bullet3.SetActive(true);
            _bullet4.SetActive(true);
            _bullet5.SetActive(true);
            _bullet6.SetActive(true);
            _bullet7.SetActive(true);
            _bullet8.SetActive(true);
            _bullet9.SetActive(true);
            _bullet10.SetActive(true);
            _bullet11.SetActive(true);
            _bullet12.SetActive(true);
            _bullet13.SetActive(true);
            _bullet14.SetActive(true);
            _bullet15.SetActive(true);
            _bullet16.SetActive(true);
        }
        else if (_currentBullets == 15)
        {
            _bullet16.SetActive(false);
        }
        else if (_currentBullets == 14)
        {
            _bullet15.SetActive(false);
        }
        else if (_currentBullets == 13)
        {

            _bullet14.SetActive(false);

        }
        else if (_currentBullets == 12)
        {

            _bullet13.SetActive(false);

        }
        else if (_currentBullets == 11)
        {

            _bullet12.SetActive(false);

        }
        else if (_currentBullets == 10)
        {

            _bullet11.SetActive(false);
  
        }
        else if (_currentBullets == 9)
        {

            _bullet10.SetActive(false);
 
        }
        else if (_currentBullets == 8)
        {

            _bullet9.SetActive(false);

        }
        else if (_currentBullets == 7)
        {

            _bullet8.SetActive(false);

        }
        else if (_currentBullets == 6)
        {

            _bullet7.SetActive(false);
   
        }
        else if (_currentBullets == 5)
        {

            _bullet6.SetActive(false);

        }
        else if (_currentBullets == 4)
        {

            _bullet5.SetActive(false);

        }
        else if (_currentBullets == 3)
        {

            _bullet4.SetActive(false);

        }
        else if (_currentBullets == 2)
        {

            _bullet3.SetActive(false);

        }
        else if (_currentBullets == 1)
        {

            _bullet2.SetActive(false);

        }
        else if (_currentBullets == 0)
        {
            _bullet1.SetActive(false);
            _bullet2.SetActive(false);
            _bullet3.SetActive(false);
            _bullet4.SetActive(false);
            _bullet5.SetActive(false);
            _bullet6.SetActive(false);
            _bullet7.SetActive(false);
            _bullet8.SetActive(false);
            _bullet9.SetActive(false);
            _bullet10.SetActive(false);
            _bullet11.SetActive(false);
            _bullet12.SetActive(false);
            _bullet13.SetActive(false);
            _bullet14.SetActive(false);
            _bullet15.SetActive(false);
            _bullet16.SetActive(false);
        }

        // RECARGAR CON R
        if (Input.GetKeyDown(KeyCode.R) && _currentBullets != _maxBullets && !_shootCooldown && GunController._akMunnition > 0)
        {
            _reloadNoise.Play();
            _shootCooldown = true;
            _animator.SetTrigger("Recargar");
            StartCoroutine(reloadSlow());
            StartCoroutine(shottyCooldown());
        }

        IEnumerator shottyCooldown()
        {
            yield return new WaitForSeconds(1);
            _shootCooldown = false;
        }

        IEnumerator reloadSlow()
        {
            yield return new WaitForSeconds(1.5f);
            GunController._akMunnition--;
            _currentBullets = _maxBullets;
            GunController._canChangeAkMunnition = true;
        }

    }



}
