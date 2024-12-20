using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemidano : MonoBehaviour
{
    public Transform player;
    [SerializeField]
   
    public float moveSpeed = 3f;
    public float damage = 10f;
    public float damageCooldown = 1f; 
    
    private float lastDamageTime = 0f;

    private void Update()
    {


        {
            Vector3 direction = (player.position - transform.position).normalized;


            Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }


            transform.position += direction * moveSpeed * Time.deltaTime;


           }
        }
       

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDamage(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDamage(collision.gameObject);
        }
    }

    private void ApplyDamage(GameObject player)
    {
        
        if (Time.time >= lastDamageTime + damageCooldown)
        {
            
            Salud playerHealth = player.GetComponent<Salud>();

            if (playerHealth != null)
            {
                
                playerHealth.ReceiveDamage(damage);
                Debug.Log("Jugador golpeado, infligiendo daño: " + damage);

                
                lastDamageTime = Time.time;
            }
        }
    }
}
