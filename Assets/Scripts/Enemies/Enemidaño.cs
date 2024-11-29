using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemidaño : MonoBehaviour
{
    public Transform player;        // Referencia al jugador
    public float moveSpeed = 3f;    // Velocidad de movimiento del enemigo
    public float damage = 10f;      // Daño que inflige al jugador

    private void Update()
    {
        // Moverse hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Orientar al enemigo hacia el jugador (corregir rotación solo en el plano XZ)
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z); // Ignorar el eje Y para evitar inclinaciones
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection); // Ajustar la rotación hacia el jugador
        }

        // Moverse hacia el jugador
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el enemigo colisionó con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reducir la salud del jugador
            Salud.health -= damage;
            Debug.Log("Jugador golpeado, salud restante: " + Salud.health);
        }
    }
}
