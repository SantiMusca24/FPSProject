using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;

    private WinCondition enemyManager;

    private void Start()
    {
        // Buscar el EnemyManager en la escena
        enemyManager = FindObjectOfType<WinCondition>();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health<=0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (enemyManager != null)
        {
            enemyManager.EnemyDied();
        }

        Destroy(gameObject);
    }
}
