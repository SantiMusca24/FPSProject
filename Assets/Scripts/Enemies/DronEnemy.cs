using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronEnemy : MonoBehaviour
  
{
    // Datos de seguimiento
    [System.Serializable]
    public struct FollowData
    {
        public Transform player;
        public float followDistance;
        public float moveSpeed;
    }

    // Datos de bombas
    [System.Serializable]
    public struct BombData
    {
        public GameObject bombPrefab;
        public Transform bombSpawnPoint;
        public float bombDropInterval;
    }

   
    public FollowData followData;
    public BombData bombData;
    private float bombTimer;

    void Start()
    {
        // Inicializamos el temporizador
        bombTimer = bombData.bombDropInterval;
    }

    void Update()
    {
        FollowPlayer();
        DropBombs();
    }

    // El dron sigue al jugador manteniendo una distancia específica
    void FollowPlayer()
    {
        // Calculamos la posición objetivo
        Vector3 targetPosition = followData.player.position + Vector3.up * followData.followDistance;

        // Calculamos la dirección
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Movemos el dron hacia la posición objetivo
        transform.position += direction * followData.moveSpeed * Time.deltaTime;
    }

    // Lanza bombas hacia el jugador
    void DropBombs()
    {
        // Disminuimos el temporizador
        bombTimer -= Time.deltaTime;

        // Verificamos si es hora de lanzar una bomba
        if (bombTimer <= 0f)
        {
            // Lanzamos una bomba
            Instantiate(bombData.bombPrefab, bombData.bombSpawnPoint.position, Quaternion.identity);

            // Reiniciamos el temporizador
            bombTimer = bombData.bombDropInterval;
        }
    }
}
