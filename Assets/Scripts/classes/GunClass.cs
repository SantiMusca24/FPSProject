using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

//TP2 - Manuel Pereiro
public class GunClass : WeaponSwitching
{


    public float damage = 10;
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

    [SerializeField] static public bool _canChangePistolMunnition = true;
    [SerializeField] static public bool _canChangeAkMunnition = true;
    [SerializeField] static public int _pistolMunnition;
    [SerializeField] static public int _akMunnition;

    [SerializeField] private TMP_Text _currentGunText;
    [SerializeField] private TMP_Text _pistolMunnitionText;
    [SerializeField] private TMP_Text _akMunnitionText;
    [SerializeField] private GameObject _pistolMunnitionObj;
    [SerializeField] private GameObject _akMunnitionObj;

    [SerializeField] private bool _canActivateInfShot = true;
    [SerializeField] static public bool _infShot = false;
    [SerializeField] protected MoveChar moveChar;
    [SerializeField] private GameObject cartel100;

    public static float extraDamage;

    public override void Start()
    {
        extraDamage = 0;
        base.Start();
        _pistolMunnition = handgun.rel;
        _akMunnition = machinegun.rel;
    }
    public override void Update()
    {
        base.Update();
        if (selectedWeapon == 0)
        {
            _currentGunText.text = "" + machinegun.name;
            damage = machinegun.dmg + extraDamage; // ACA SE DEFINE EL DAÑO DEL RIFLE. PARA SUMAR DAÑO PODÉS CAMBIAR DIRECTAMENTE LA VARIABLE "extraDamage"
            _pistolMunnitionObj.SetActive(false);
            _akMunnitionObj.SetActive(true);
        }
        else if (selectedWeapon == 1)
        {
            _currentGunText.text = "" + handgun.name;
            damage = handgun.dmg + extraDamage; // ACA SE DEFINE EL DAÑO DE LA PISTOLA. PARA SUMAR DAÑO PODÉS CAMBIAR DIRECTAMENTE LA VARIABLE "extraDamage"
            _pistolMunnitionObj.SetActive(true);
            _akMunnitionObj.SetActive(false);
        }

        if (_infShot)
        {
            BulletCounter._currentBullets = machinegun.amm;
            PistolBullets._currentBullets = handgun.amm;
        }

        if (Input.GetKeyDown(KeyCode.L) && _canActivateInfShot)
        {
            _canActivateInfShot = false;
            _infShot = true;
            StartCoroutine(infShotThing());
        }

        if (PistolBullets._currentBullets <= 0 && _canChangePistolMunnition && _pistolMunnition > 0)
        {
            _canChangePistolMunnition = false;
            //_pistolMunnition--;
        }
        if (BulletCounter._currentBullets <= 0 && _canChangeAkMunnition && _akMunnition > 0)
        {
            _canChangeAkMunnition = false;
            //_akMunnition--;
        }
        if (Input.GetKeyDown(KeyCode.E) && canActivateDoubleShot)
        {
            ActivateDoubleShot(doubleShotDuration);
        }
        //if (_akMunnition <= 0) BulletCounter._canShoot = false;
        //if (_pistolMunnition <= 0) PistolBullets._canShoot = false;
        
        _pistolMunnitionText.text = "x" + _pistolMunnition;
        _akMunnitionText.text = "x" + _akMunnition;


        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire && BulletCounter._canShoot && selectedWeapon == 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            if (isDoubleShotActive)
            {
                Invoke("Shoot", 0.1f); // Disparo secundario con un pequeño retraso
            }
        }
        else if (Input.GetMouseButtonDown(0) && selectedWeapon == 1 && PistolBullets._canShoot && !PistolBullets._shootCooldown)
        {
            Shoot();
            _animator.SetTrigger("Fire");
            if (isDoubleShotActive)
            {
                Invoke("Shoot", 0.1f); // Disparo secundario con un pequeño retraso
            }
        }
        else if (Input.GetMouseButtonDown(0) && !PistolBullets._shootCooldown) _emptyNoise.Play();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            InteractBulletBox();
            InteractM4();
            InteractKey();
        }
    }

    void InteractBulletBox()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
          //  Debug.Log(hit.transform.name);


            bulletBox target = hit.transform.GetComponent<bulletBox>();
            if (target != null)
            {
                target.Interact();
            }
        }
    }
    void InteractM4()
    {
        //TPS Santiago Muscatiello (uso de diccionario para acumlar puntos y poder recoger arma)
        if (moveChar.points < 100) 
        {
            Debug.Log("Necesitas al menos 100 puntos para interactuar con el M4.");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
           // Debug.Log("activo");


            GunFloor target = hit.transform.GetComponent<GunFloor>();
            if (target != null)
            {
                target.Interact();
                moveChar.points -=100;
                cartel100.SetActive(false);
                Debug.Log($"Has usado 100 puntos. Puntos restantes: {moveChar.points}");
            }
        }
        
    }
    void InteractKey()
    {
       
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
           // Debug.Log("activo");


            key target = hit.transform.GetComponent<key>();
            if (target != null)
            {
                target.Interact();
            }
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        muzzleFlash2.Play();
        _shootNoise.Play();
        if (selectedWeapon == 0) BulletCounter._currentBullets--;
        else if (selectedWeapon == 1) PistolBullets._currentBullets--;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);


            EnemyHealthClass target = hit.transform.GetComponent<EnemyHealthClass>();
            if (target != null && target.TakeDamage(damage))
            {
                //TP2 - Santiago Muscatiello (diccionario)
                EnemyHealthClass enemy = target as EnemyHealthClass;
                if (enemy != null)
                {
                    moveChar.GetLoot(EnemyScoreManager.Instance.GetPoints(enemy.enemyType));
                }
                  
            }

            OrbClass target2 = hit.transform.GetComponent<OrbClass>();
            if (target2 != null)
            {
                //TPS Santiago Muscatiello (diccionario)
                OrbClass enemy2 = target2 as OrbClass;
                if (enemy2 != null)
                {
                    enemy2.Interact(); 
                }

            }

            /*EnemyHealth targetDron = hit.transform.GetComponent<EnemyHealth>();
            if (targetDron != null)
            {
                targetDron.TakeDamage(damage);
            }*/

            GameObject impactGO =Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            MineScript target = hit.transform.GetComponent<MineScript>();
            if (target != null)
            {
                target.explodeee();
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
    IEnumerator infShotThing()
    {
        yield return new WaitForSeconds(5);
        _infShot = false;
        _canActivateInfShot = true;
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
