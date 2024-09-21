using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash; public ParticleSystem muzzleFlash2;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    [SerializeField] private AudioSource _shootNoise, _emptyNoise;
    [SerializeField] private Animator _animator;
    private bool isDoubleShotActive = false;
    private float doubleShotDuration = 5f; // Duración del power-up de doble disparo
    private bool canActivateDoubleShot = true;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && canActivateDoubleShot)
        {
            ActivateDoubleShot(doubleShotDuration);
        }



        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire && BulletCounter._canShoot && WeaponSwitching.selectedWeapon == 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            if (isDoubleShotActive)
            {
                Invoke("Shoot", 0.1f); // Disparo secundario con un pequeño retraso
            }
        }
        else if (Input.GetMouseButtonDown(0) && WeaponSwitching.selectedWeapon == 1 && PistolBullets._canShoot)
        {
            Shoot();
            _animator.SetTrigger("Fire");
            if (isDoubleShotActive)
            {
                Invoke("Shoot", 0.1f); // Disparo secundario con un pequeño retraso
            }
        }
        else if (Input.GetMouseButtonDown(0)) _emptyNoise.Play();
    }

    void Shoot()
    {
        muzzleFlash.Play();
        muzzleFlash2.Play();
        _shootNoise.Play();
        if (WeaponSwitching.selectedWeapon == 0) BulletCounter._currentBullets--;
        else if (WeaponSwitching.selectedWeapon == 1) PistolBullets._currentBullets--;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);


            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target !=null)
            {
                target.TakeDamage(damage);
            }
            GameObject impactGO =Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
        
    }
    public void ActivateDoubleShot(float duration)
    {
        isDoubleShotActive = true;
        canActivateDoubleShot = false;
        Invoke("DeactivateDoubleShot", duration); // Desactiva el power-up después de la duración
    }

    // Método para desactivar el power-up
    void DeactivateDoubleShot()
    {
        isDoubleShotActive = false;
        canActivateDoubleShot = true;
    }
}
