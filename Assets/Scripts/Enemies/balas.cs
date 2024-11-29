using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balas : MonoBehaviour
{
    public float damage = 10f; // Daño que causa el proyectil
    public float lifeTime = 5f; // Tiempo antes de que el proyectil desaparezca

    private void Start()
    {
        // Destruir la instancia del proyectil después de un tiempo
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si el proyectil impacta al jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reducir la salud del jugador
            Salud.health -= damage;

            // Destruir el proyectil después del impacto
            Destroy(gameObject);
        }
        else
        {
            // Destruir el proyectil si impacta otra superficie
            Destroy(gameObject);
        }
    }
}
