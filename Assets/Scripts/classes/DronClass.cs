using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DronClass : EnemyHealthClass
{
    private bool escape = false;

    //TP2 - Manuel Pereiro
    protected override void Awake()
    {
        base.Awake();
        followDistance = 10f;
        moveSpeed = 5f;
        bombDropInterval = 5f;

        health = 30;
        halfHealth = 20;
        debugTest = "Dron";
        bombTimer = bombDropInterval;
    }
  
    

    void Update()
    {
        if (!escape) FollowPlayer();
        else LeavePlayer();

        DropBombs();
    }

    // El dron sigue al jugador manteniendo una distancia específica
    void FollowPlayer()
    {
        Vector3 targetPosition = player.position + Vector3.up * followDistance; 
        Vector3 direction = (targetPosition - transform.position).normalized;   
        transform.position += direction * moveSpeed * Time.deltaTime;           
    }
    void LeavePlayer()
    {
        Vector3 targetPosition2 = player.position + Vector3.up * followDistance;
        Vector3 direction2 = (targetPosition2 - transform.position).normalized;
        transform.position -= direction2 * moveSpeed * Time.deltaTime;
    }

    // Lanza bombas hacia el jugador
    void DropBombs()
    {
        bombTimer -= Time.deltaTime;

        if (bombTimer <= 0f)
        {
            // Lanza una bomba
            Instantiate(bombPrefab, bombSpawnPoint.position, Quaternion.identity);
            bombTimer = bombDropInterval; // Reinicia el temporizador
        }
    }

    protected override void HalfHealth()
    {

        escape = true;

    }
}
