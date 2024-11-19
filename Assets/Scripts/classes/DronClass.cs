using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DronClass : EnemyHealthClass
{
    //Lorenzo Marmol TP2 (structs)
    public struct DronSettings
    {
        public float followDistance;
        public float moveSpeed;
        public float bombDropInterval; 
        public bool startedCounting;
        public float bombTimer;
        public int health;
        public int halfHealth;
        public bool escape;
        public bool swaying;
    }


    public DronSettings dronSettings;
    private bool escape = false;
    private bool swaying = false;
    private bool startedCounting = false;

    //TP2 - Manuel Pereiro
    protected override void Awake()
    {
        base.Awake();
        
        dronSettings.followDistance = 10f;
        dronSettings.moveSpeed = 5f;
        dronSettings.bombDropInterval = 5f;
        dronSettings.escape = false;
        dronSettings.swaying = false;
        dronSettings.startedCounting = false;

        dronSettings.health = 40;
        dronSettings.halfHealth = 20;
        debugTest = "Dron";
        dronSettings.bombTimer = dronSettings.bombDropInterval;
    }
  
    

    void Update()
    {
        if (!dronSettings.escape) FollowPlayer();
        else LeavePlayer();

        DropBombs();
    }

    // El dron sigue al jugador manteniendo una distancia específica
    void FollowPlayer()
    {
        if (dronSettings.swaying)
        {
            if (!dronSettings.startedCounting)
            {
                StartCoroutine(returning());
            }
        }        
        Vector3 targetPosition = player.position + Vector3.up * dronSettings.followDistance; 
        Vector3 direction = (targetPosition - transform.position).normalized;   
        transform.position += direction * dronSettings.moveSpeed * Time.deltaTime;           
    }
    void LeavePlayer()
    {
        if (!dronSettings.startedCounting)
        {
            StartCoroutine(leaving());
        }
        dronSettings.swaying = true;
        StartCoroutine(leaving());
        Vector3 targetPosition2 = player.position + Vector3.up * dronSettings.followDistance;
        Vector3 direction2 = (targetPosition2 - transform.position).normalized;
        transform.position -= direction2 * dronSettings.moveSpeed * Time.deltaTime;
    }

    IEnumerator leaving()
    {
        dronSettings.startedCounting = true;
        yield return new WaitForSeconds(5);
        startedCounting = false;
        dronSettings.escape = false;
    }
    IEnumerator returning()
    {
        dronSettings.startedCounting = true;
        yield return new WaitForSeconds(5);
        dronSettings.startedCounting = false;
        dronSettings.escape = true;
    }

    // Lanza bombas hacia el jugador
    void DropBombs()
    {
        dronSettings.bombTimer -= Time.deltaTime;

        if (dronSettings.bombTimer <= 0f)
        {
            // Lanza una bomba
            Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
            dronSettings.bombTimer = dronSettings.bombDropInterval; // Reinicia el temporizador
        }
    }

    protected override void HalfHealth()
    {

        dronSettings.escape = true;

    }
}
