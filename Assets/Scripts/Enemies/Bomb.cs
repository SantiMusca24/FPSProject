using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float damageRadius = 2f;   
    public float explosionForce = 700f;
    public float bombDamage = 20f;
    private Vector3 explosionPosition;
    public ParticleSystem explosion;
    public MeshRenderer mesh;
    public AudioSource bomba;
    void OnCollisionEnter(Collision collision)
    {
        explosionPosition = transform.position;
        Explode();
    }

    void Explode()
    {
        explosion.Play();
        bomba.Play();
        
        GetComponent<Collider>().enabled = false;
        
        Collider[] objectsInRange = Physics.OverlapSphere(explosionPosition, damageRadius);

        foreach (Collider obj in objectsInRange)
        {
            
            float distance = Vector3.Distance(explosionPosition, obj.transform.position);
            Debug.Log("Distancia entre la explosión y el objeto: " + distance);
            if (distance <= damageRadius) 
            {
               
                if (Salud.health > 0)
                {
                   
                    Salud.health -= bombDamage;

                    if (Salud.health < 0)
                    {
                        Salud.health = 0;
                    }

                    Debug.Log("Daño aplicado: " + bombDamage + ". Salud restante: " + Salud.health);
                }

               
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
                }
            }
        }

        float particleDuration = explosion.main.duration;

        
        // Destruir al padre con retraso, si existe
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject, particleDuration);
            mesh.enabled = false;
        }

        // Destruir el propio objeto con retraso
        Destroy(gameObject, particleDuration);

    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }


}
