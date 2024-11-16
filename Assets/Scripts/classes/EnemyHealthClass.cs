using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthClass : MonoBehaviour
{
    public Transform player;
    public float followDistance;
    public float moveSpeed;
    public GameObject bombPrefab;
    public Transform bombSpawnPoint;
    public float bombDropInterval;

    public float bombTimer;

    public float health;

    private WinCondition enemyManager;

    public string debugTest;

    private void Start()
    {
        // Buscar el EnemyManager en la escena
        enemyManager = FindObjectOfType<WinCondition>();
        
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(debugTest + "health: " + health);
        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        if (enemyManager != null)
        {
            Debug.Log("enemyManager != null");
            enemyManager.EnemyDied();
        }
        //if (enemyManager == null) Debug.Log("sasfsd");

        Destroy(gameObject);
    }

}
