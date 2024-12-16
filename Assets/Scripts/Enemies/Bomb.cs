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


    void Explode()
    {
        explosion.Play();
        bomba.Play();

        GetComponent<Collider>().enabled = false;

        Collider[] objectsInRange = Physics.OverlapSphere(explosionPosition, damageRadius);

        foreach (Collider obj in objectsInRange)
        {
            Debug.Log("Objeto en rango: " + obj.name);

            
            Salud salud = obj.GetComponentInParent<Salud>();
            if (salud != null)
            {
                Debug.Log($"Aplicando daño de {bombDamage} a {obj.name}");
                salud.ReceiveDamage(bombDamage);
            }
            else
            {
                Debug.Log($"El objeto {obj.name} no tiene el componente Salud");
            }

            
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
            }
        }

        float particleDuration = explosion.main.duration;

        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject, particleDuration);
            mesh.enabled = false;
        }

        Destroy(gameObject, particleDuration);
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        Debug.Log($"Bomba colisionó con {collision.gameObject.name}");
        explosionPosition = transform.position;
        Explode();
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }


}
