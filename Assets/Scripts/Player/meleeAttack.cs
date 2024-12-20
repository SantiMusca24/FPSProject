using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    [SerializeField] private Collider meleeCollider; 
    
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private MoveChar moveChar;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject cuchillo;
    [SerializeField] private WeaponSwitching weaponManager;
    static public bool meleeActivate;
    static public int damage = 30;
    [SerializeField] private ParticleSystem impactParticle;
    [SerializeField] private AudioSource meleeAttackSound;

    private bool canAttack = true; 

    void Start()
    {
        meleeCollider.enabled = false; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && canAttack && WeaponSwitching.isBazookaAvailable==false)
        {
            StartCoroutine(PerformMeleeAttack());
        }
    }

    private IEnumerator PerformMeleeAttack()
    {
        meleeActivate = true;
        weaponManager.DeactivateAllWeapons();
        cuchillo.SetActive(true);
        canAttack = false;
        if (meleeAttackSound != null)
        {
            meleeAttackSound.Play();
        }
        meleeCollider.enabled = true;
        animator.SetTrigger("MeleeAttack");

        yield return new WaitForSeconds(0.5f);

        meleeCollider.enabled = false;
        cuchillo.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        weaponManager.ActivateSelectedWeapon();
        meleeActivate = false;
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        EnemyHealthClass target = other.GetComponent<EnemyHealthClass>();
        if (target != null)
        {
            bool enemyKilled = target.TakeDamage(damage); 

            
            Debug.Log($"Daño infligido al enemigo: {target.name}.");
            ParticleSystem particle = Instantiate(impactParticle, other.transform.position, Quaternion.identity);
            particle.Play();
            Destroy(particle.gameObject, 2f); // Destruir las partículas tras 2 segundos
            if (enemyKilled)
            {
               
                Debug.Log($"El enemigo {target.name} ha sido derrotado.");
                moveChar.GetLoot(EnemyScoreManager.Instance.GetPoints(target.enemyType));
            }
        }
    }
}
