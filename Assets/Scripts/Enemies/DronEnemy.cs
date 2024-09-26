using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronEnemy : MonoBehaviour
{
    public Transform player;             
    public float followDistance = 10f;   
    public float moveSpeed = 5f;         
    public GameObject bombPrefab;        
    public Transform bombSpawnPoint;    
    public float bombDropInterval = 5f;  

    private float bombTimer;             

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
