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
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    [SerializeField] private AudioSource _shootNoise, _emptyNoise;


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire && BulletCounter._canShoot)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if (Input.GetMouseButtonDown(0) && !BulletCounter._canShoot) _emptyNoise.Play();
    }

    void Shoot()
    {
        muzzleFlash.Play();
        _shootNoise.Play();
        BulletCounter._currentBullets--;
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
}
