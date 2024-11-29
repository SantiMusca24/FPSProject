using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shootenemi : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float followRange = 10f; // Distancia máxima para seguir al jugador
    public float attackRange = 5f; // Distancia mínima para atacar al jugador
    public float moveSpeed = 3f; // Velocidad de movimiento del enemigo
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform shootPoint; // Punto desde donde se dispara el proyectil
    public float projectileSpeed = 10f; // Velocidad del proyectil
    public float attackCooldown = 2f; // Tiempo entre ataques

    private float lastAttackTime;

    void Update()
    {
        // Calcular la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador está dentro del rango de seguimiento, moverse hacia él
        if (distanceToPlayer <= followRange && distanceToPlayer > attackRange)
        {
            FollowPlayer();
        }

        // Si el jugador está dentro del rango de ataque, lanzar proyectil
        if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            ShootProjectile();
        }
    }

    private void FollowPlayer()
    {
        // Moverse hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z)); // Mirar hacia el jugador
    }

    private void ShootProjectile()
    {
        lastAttackTime = Time.time;

        // Crear una nueva instancia del proyectil
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Configurar la velocidad y dirección del proyectil
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = (player.position - shootPoint.position).normalized * projectileSpeed;

        Debug.Log("Proyectil disparado hacia el jugador");
    }
}
