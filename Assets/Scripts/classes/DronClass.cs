using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronClass : EnemyHealthClass
{
    

    private void Awake()
    {
        followDistance = 10f;
        moveSpeed = 5f;
        bombDropInterval = 5f;

        health = 25;
        debugTest = "Dron";
    }
    void Start()
    {
        bombTimer = bombDropInterval; // Inicializamos el temporizador
    }

    void Update()
    {
        FollowPlayer();
        DropBombs();
    }

    // El dron sigue al jugador manteniendo una distancia específica
    void FollowPlayer()
    {
        Vector3 targetPosition = player.position + Vector3.up * followDistance; 
        Vector3 direction = (targetPosition - transform.position).normalized;   
        transform.position += direction * moveSpeed * Time.deltaTime;           
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
}
