using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullets : MonoBehaviour
{
    [SerializeField] static public int _currentBullets; // BALAS ACTUALES
    [SerializeField] static public int _maxBullets = 6; // BALAS MÁXIMAS
    [SerializeField]
    private GameObject _bullet1, _bullet2, _bullet3, _bullet4, _bullet5, _bullet6;  // ÍCONOS DE BALAS 
    [SerializeField] static public bool _shootCooldown = false; // EL JUGADOR NO PODRÁ DISPARAR CUANDO EL COOLDOWN ESTÉ ACTIVO
    [SerializeField] private AudioSource _reloadNoise;
    [SerializeField] static public bool _canShoot = true;
    [SerializeField] private Animator _animator;

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

        if (_currentBullets <= 0) _canShoot = false;
        else _canShoot = true;

        // CADA NÚMERO DE BALAS DEFINE CUÁLES ÍCONOS ESTÁN ACTIVOS
        if (_currentBullets == 6)
        {
            _bullet1.SetActive(true);
            _bullet2.SetActive(true);
            _bullet3.SetActive(true);
            _bullet4.SetActive(true);
            _bullet5.SetActive(true);
            _bullet6.SetActive(true);
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
        }

        // RECARGAR CON R
        if (Input.GetKeyDown(KeyCode.R) && _currentBullets != _maxBullets && !_shootCooldown)
        {
            _reloadNoise.Play();
            _shootCooldown = true;
            _animator.SetTrigger("Reload");
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
            _currentBullets = _maxBullets;
        }

    }



}
