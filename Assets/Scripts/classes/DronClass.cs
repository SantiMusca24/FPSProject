using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DronClass : EnemyHealthClass
{
    private bool escape = false;
    private bool swaying = false;
    private bool startedCounting = false;
    public AudioSource bombacae;
    public AudioSource dronmueve;
    private bool isMoving = false;
    public float detectionRangeMin = 5f;  // Distancia mínima para que el dron comience a seguir
    public float detectionRangeMax = 20f;
    //TP2 - Manuel Pereiro
    protected override void Awake()
    {
        base.Awake();
        followDistance = 10f;
        moveSpeed = 2f;
        bombDropInterval = 5f;
        escape = false;
        swaying = false;
        startedCounting = false;

        health = 40 ;
        halfHealth = 20;
        debugTest = "Dron";
        bombTimer = bombDropInterval;
    }
  
    

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Solo sigue al jugador si está dentro del rango de detección
        if (distanceToPlayer > detectionRangeMin && distanceToPlayer < detectionRangeMax && !escape)
        {
            FollowPlayer();
        }
        

        DropBombs();
        HandleDronSound();
    }


    // El dron sigue al jugador manteniendo una distancia específica
    void FollowPlayer()
    {
        if (swaying)
        {
            if (!startedCounting)
            {
                StartCoroutine(returning());
            }
        }

        Vector3 targetPosition = player.position + Vector3.up * followDistance;
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        isMoving = true;
    }
    void LeavePlayer()
    {
        if (!startedCounting)
        {
            StartCoroutine(leaving());
        }
        swaying = true;
        StartCoroutine(leaving());
        Vector3 targetPosition2 = player.position + Vector3.up * followDistance;
        Vector3 direction2 = (targetPosition2 - transform.position).normalized;
        transform.position -= direction2 * moveSpeed * Time.deltaTime;
        isMoving = true;
    }

    IEnumerator leaving()
    {
        startedCounting = true;
        yield return new WaitForSeconds(5);
        startedCounting = false;
        escape = false;
        isMoving = false;
    }
    IEnumerator returning()
    {
        startedCounting = true;
        yield return new WaitForSeconds(5);
        startedCounting = false;
        escape = true;
    }

    // Lanza bombas hacia el jugador
    void DropBombs()
    {
        if (!isMoving) return; // No lanza bombas si no está en movimiento

        bombTimer -= Time.deltaTime;

        if (bombTimer <= 0f)
        {
            bombacae.Play();
            // Lanza una bomba
            Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
            bombTimer = bombDropInterval; // Reinicia el temporizador
        }
    }

    protected override void HalfHealth()
    {

        escape = true;

    }
    void HandleDronSound()
    {
        if (isMoving && !dronmueve.isPlaying)
        {
            dronmueve.Play(); // Inicia el sonido si está en movimiento y no está reproduciendo
        }
        else if (!isMoving && dronmueve.isPlaying)
        {
            dronmueve.Stop(); // Detiene el sonido si ya no se está moviendo
        }
    }
}
