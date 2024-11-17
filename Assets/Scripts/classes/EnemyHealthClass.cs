using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 - Manuel Pereiro

public abstract class EnemyHealthClass : MonoBehaviour
{
    public Transform player;
    protected float followDistance;
    protected float moveSpeed;
    public GameObject bombPrefab;
    public Transform bombSpawnPoint;
    protected float bombDropInterval;

    protected float bombTimer;

    protected float health;
    public float halfHealth;

    private WinCondition enemyManager;

    protected AudioSource gruntz;
    protected string debugTest;
    private bool didHalfHealth = false;

    protected virtual void Awake()
    {
        // Buscar el EnemyManager en la escena
        enemyManager = FindObjectOfType<WinCondition>();
        
    }
    public void TakeDamage(float amount)
    {
        if (health <= halfHealth && !didHalfHealth)
        {
            //Debug.Log("TAL");
            HalfHealth();
            didHalfHealth = true;
        }        
        if (health > 0)
        {
            health -= amount;
            Debug.Log(debugTest + "health: " + health);
        }
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
        else Debug.Log("NULL");
        //if (enemyManager == null) Debug.Log("sasfsd");

        Destroy(gameObject);
    }

    protected abstract void HalfHealth();

}
