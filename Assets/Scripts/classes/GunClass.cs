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
    public GameObject impactEffect2;
    public GameObject impactEffect3;
    public GameObject impactEffect4;
    public GameObject impactEffectBazooka;

    private float nextTimeToFire = 0f;

    [SerializeField] private AudioSource _shootNoise, _emptyNoise, _bazookaNoise, _bazookaExplode;
    [SerializeField] private Animator _animator;
    private bool isDoubleShotActive = false;
    private bool isInstaKillActive = false;
    private float doubleShotDuration = 5f;
    private float instaKillDuration = 5f;// Duraci�n del power-up de doble disparo 
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
    [SerializeField] static public bool _doubleShot = false;
    [SerializeField] static public bool _instakill =  false;
    [SerializeField] protected MoveChar moveChar;
    [SerializeField] private GameObject cartel100;
    [SerializeField] private GameObject hud1;
    [SerializeField] private GameObject hud2;
    [SerializeField] private GameObject hud3;
    


    public AudioSource pickupSound; 
    public float closeRange = 2f;   

    public static float extraDamage;

    public override void Start()
    {
        extraDamage = 0;
        base.Start();
        _pistolMunnition = handgun.rel;
        _akMunnition = machinegun.rel;
        _infShot = false;
        hud1.SetActive(false);
        hud2.SetActive(false);
        hud3.SetActive(false);
        

    }
    public override void Update()
    {
        base.Update();
        if (selectedWeapon == 0)
        {
            _currentGunText.text = "" + machinegun.name;
            damage = machinegun.dmg + extraDamage; // ACA SE DEFINE EL DA�O DEL RIFLE. PARA SUMAR DA�O POD�S CAMBIAR DIRECTAMENTE LA VARIABLE "extraDamage"
            _pistolMunnitionObj.SetActive(false);
            _akMunnitionObj.SetActive(true);
        }   
        else if (selectedWeapon == 1)
        {
            _currentGunText.text = "" + handgun.name;
            damage = handgun.dmg + extraDamage; // ACA SE DEFINE EL DA�O DE LA PISTOLA. PARA SUMAR DA�O POD�S CAMBIAR DIRECTAMENTE LA VARIABLE "extraDamage"
            _pistolMunnitionObj.SetActive(true);
            _akMunnitionObj.SetActive(false);
        }
        else if (selectedWeapon == 2)
        {
            _currentGunText.text = "" + bazooka.name;
            damage = bazooka.dmg + extraDamage;
            _pistolMunnitionObj.SetActive(false);
            _akMunnitionObj.SetActive(false);
        }

        if (_infShot)
        {
            BulletCounter._currentBullets = machinegun.amm;
            PistolBullets._currentBullets = handgun.amm;
            BazookaBullet._currentBullets = bazooka.amm;
            StartCoroutine(infShotThing());
            _canActivateInfShot = false;

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
        if (_doubleShot)
        {
            ActivateDoubleShot(doubleShotDuration);
            canActivateDoubleShot = false;
        }
        if (_instakill)
        {
            Debug.Log("llama instakill");
            ActivateInstaKill(instaKillDuration);

        }
        //if (_akMunnition <= 0) BulletCounter._canShoot = false;
        //if (_pistolMunnition <= 0) PistolBullets._canShoot = false;
        
        _pistolMunnitionText.text = "x" + _pistolMunnition;
        _akMunnitionText.text = "x" + _akMunnition;


        if (!Input.GetKey(KeyCode.LeftControl) && Input.GetButton("Fire1") && Time.time > nextTimeToFire && BulletCounter._canShoot && selectedWeapon == 0 && meleeAttack.meleeActivate==false)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            if (isDoubleShotActive)
            {
                Invoke("Shoot", 0.1f); // Disparo secundario con un peque�o retraso
            }
        }
        else if (Input.GetMouseButtonDown(0) && selectedWeapon == 1 && PistolBullets._canShoot && !PistolBullets._shootCooldown && meleeAttack.meleeActivate == false)
        {
            Shoot();
            _animator.SetTrigger("Fire");
            if (isDoubleShotActive)
            {
                Invoke("Shoot", 0.1f); // Disparo secundario con un peque�o retraso
            }
        }
        else if (Input.GetButton("Fire1") && Time.time > nextTimeToFire && BazookaBullet._canShoot && selectedWeapon == 2)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            if (isDoubleShotActive)
            {
                Invoke("Shoot", 0.1f); // Disparo secundario con un peque�o retraso
            }
        }
        else if (Input.GetMouseButtonDown(0) && !PistolBullets._shootCooldown && meleeAttack.meleeActivate == false) _emptyNoise.Play();

        if (Input.GetKeyDown(KeyCode.F))
        {
            InteractBulletBox();
            InteractM4();
            InteractBazooka();
            InteractKey();
            InteractKeyWin();
            InteractInfiniteShoot();
            InteractDoubleShoot();
            InteractInstaKill();
        }
    }
    void InteractKeyWin()
    {
        if (moveChar.points < 100)
        {
            Debug.Log("Necesitas al menos 100 puntos para interactuar con el M4.");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, closeRange))
        {
            //  Debug.Log(hit.transform.name);


            keyWin target = hit.transform.GetComponent<keyWin>();
            if (target != null)
            {
                target.Interact();
                moveChar.points -= 100;
                if (pickupSound != null)
                {
                    pickupSound.Play();
                }
            }
        }
    }
    void InteractDoubleShoot()
    {
        if (moveChar.points < 300)
        {
            Debug.Log("Necesitas al menos 100 puntos para interactuar con el M4.");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, closeRange))
        {
            //  Debug.Log(hit.transform.name);


            DoubleShotFloor target = hit.transform.GetComponent<DoubleShotFloor>();
            if (target != null)
            {
                target.Interact();
                moveChar.points -= 300;
                if (pickupSound != null)
                {
                    pickupSound.Play();
                }
            }
        }
    }
    void InteractInstaKill()
    {
        if (moveChar.points < 300)
        {
            Debug.Log("Necesitas al menos 300 puntos para interactuar con el M4.");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, closeRange))
        {
            //  Debug.Log(hit.transform.name);


            instakillfloor target = hit.transform.GetComponent<instakillfloor>();
            if (target != null)
            {
                target.Interact();
                moveChar.points -= 300;
                if (pickupSound != null)
                {
                    pickupSound.Play();
                }
            }
        }
    }
    void InteractInfiniteShoot()
    {
        if (moveChar.points < 300)
        {
            Debug.Log("Necesitas al menos 100 puntos para interactuar con el M4.");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, closeRange))
        {
            //  Debug.Log(hit.transform.name);


            InfiniteShootFlor target = hit.transform.GetComponent<InfiniteShootFlor>();
            if (target != null)
            {
                target.Interact();
                moveChar.points -= 300;
                if (pickupSound != null)
                {
                    pickupSound.Play();
                }
            }
        }
    }
    void InteractBazooka()
    {
        if (moveChar.points < 200)
        {
            Debug.Log("Necesitas al menos 100 puntos para interactuar con el M4.");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, closeRange))
        {
            //  Debug.Log(hit.transform.name);


            BazookaFloor target = hit.transform.GetComponent<BazookaFloor>();
            if (target != null)
            {
                target.Interact();
                moveChar.points -= 200;
                if (pickupSound != null)
                {
                    pickupSound.Play();
                }
            }
        }
    }
    void InteractBulletBox()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, closeRange))
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
        // Verifica si el jugador tiene suficientes puntos
        if (moveChar.points < 100)
        {
            Debug.Log("Necesitas al menos 100 puntos para interactuar con el M4.");
            return;
        }

        RaycastHit hit;
        // Realiza el Raycast con la distancia reducida
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, closeRange))
        {
            GunFloor target = hit.transform.GetComponent<GunFloor>();
            if (target != null)
            {
                // Interacci�n con el arma
                target.Interact();
                moveChar.points -= 100;

                // Desactiva el cartel
                cartel100.SetActive(false);

                // Reproduce el sonido de recogida del M4
                if (pickupSound != null)
                {
                    pickupSound.Play();
                }

                Debug.Log($"Has usado 100 puntos. Puntos restantes: {moveChar.points}");
            }
        }
        else
        {
            Debug.Log("No est�s lo suficientemente cerca para recoger el M4.");
        }
    }
    void InteractKey()
    {
       
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, closeRange))
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
        if (selectedWeapon != 2)
        {
            _shootNoise.Play();
        }
        else _bazookaNoise.Play();

        if (selectedWeapon == 0) BulletCounter._currentBullets--;
        else if (selectedWeapon == 1) PistolBullets._currentBullets--;
        else if (selectedWeapon == 2) BazookaBullet._currentBullets--;
        int layerMask = ~LayerMask.GetMask("IgnoreRaycast");
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range,layerMask))
        {
            Debug.Log(hit.transform.name);

            // Verificar si golpea un enemigo
            EnemyHealthClass target = hit.transform.GetComponent<EnemyHealthClass>();
            if (target != null && target.TakeDamage(damage))
            {
                EnemyHealthClass enemy = target as EnemyHealthClass;
                if (enemy != null)
                {
                    moveChar.GetLoot(EnemyScoreManager.Instance.GetPoints(enemy.enemyType));
                }
            }
            // Verificar si golpea una esfera u objeto especial
            OrbClass target2 = hit.transform.GetComponent<OrbClass>();
            if (target2 != null)
            {
                target2.Interact();
            }

            // Si el impacto no es un enemigo o interactivo, generar el efecto de impacto.
            if (hit.collider.name.ToLower().Contains("suelo"))
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
            else if (hit.collider.name.ToLower().Contains("dron"))
            {
                // Efecto exclusivo para el dron
                GameObject impactGO = Instantiate(impactEffect3, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
            else if (hit.collider.name.ToLower().Contains("soldado"))
            {
                GameObject impactGO = Instantiate(impactEffect4, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
            else
            {
                GameObject impactGO = Instantiate(impactEffect2, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }

            if (selectedWeapon == 2)
            {
                _bazookaExplode.Play();
                GameObject impactGO = Instantiate(impactEffectBazooka, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 4f);
            }
        }

       
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            MineScript target = hit.transform.GetComponent<MineScript>();
            if (target != null)
            {
                target.explodeee();
            }
           

        }
    }
    IEnumerator infShotThing()
    {
        hud2.SetActive(true);
        yield return new WaitForSeconds(5);
        _infShot = false;
        hud2.SetActive(false);
        
    }
    IEnumerator doubleShotThing()
    {
        Debug.Log("doubleshotthing");
        hud3.SetActive(true);
        yield return new WaitForSeconds(5);
        _doubleShot = false;
        isDoubleShotActive = false;
        hud3.SetActive(false);
    }
    public void ActivateDoubleShot(float duration)
    {
        isDoubleShotActive = true;
        canActivateDoubleShot = false;
        StartCoroutine(doubleShotThing());
    }

    public void ActivateInstaKill(float duration)
    {
        Debug.Log("instakill activo");
        meleeAttack.damage = 100;
        isInstaKillActive = true;
        StartCoroutine(instaKillThing());
    }
    IEnumerator instaKillThing()
    {
        Debug.Log("instakill");
        hud1.SetActive(true);
        yield return new WaitForSeconds(5);
        isInstaKillActive = false;
        _instakill = false;
        meleeAttack.damage = 30;
        hud1.SetActive(false);
        Debug.Log("fin");
    }


}
