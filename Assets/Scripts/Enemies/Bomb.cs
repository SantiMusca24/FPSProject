using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float damageRadius = 2f;   
    public float explosionForce = 700f;
    public float bombDamage = 20f;
    private Vector3 explosionPosition;

    void OnCollisionEnter(Collision collision)
    {
        explosionPosition = transform.position;
        Explode();
    }

    void Explode()
    {
        GetComponent<Collider>().enabled = false;
        // Obtener todos los objetos dentro del radio de la explosi�n
        Collider[] objectsInRange = Physics.OverlapSphere(explosionPosition, damageRadius);

        foreach (Collider obj in objectsInRange)
        {
            // Verificar la distancia entre la explosi�n y el objeto
            float distance = Vector3.Distance(explosionPosition, obj.transform.position);
            Debug.Log("Distancia entre la explosi�n y el objeto: " + distance);
            if (distance <= damageRadius) // Asegurarse de que el objeto est� dentro del radio
            {
                // Aplicar da�o si la salud es mayor a 0
                if (Salud.health > 0)
                {
                    // Aplicar da�o
                    Salud.health -= bombDamage;

                    if (Salud.health < 0)
                    {
                        Salud.health = 0;
                    }

                    Debug.Log("Da�o aplicado: " + bombDamage + ". Salud restante: " + Salud.health);
                }

                // Aplicar fuerza de explosi�n a objetos con Rigidbody
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
                }
            }
        }

        // Destruir el objeto explosivo
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar el radio de da�o de la explosi�n
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }


}
