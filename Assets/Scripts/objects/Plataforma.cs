using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float disappearDelay = 2f;  
    public float reappearDelay = 3f;   
    private Renderer platformRenderer;
    private Collider platformCollider;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.GetComponent<MoveChar>() != null)
        {
            Debug.Log("Jugador tocó la plataforma. Plataforma desaparecerá en " + disappearDelay + " segundos.");
            
            StartCoroutine(DisappearPlatform());
        }
    }

    IEnumerator DisappearPlatform()
    {
        
        yield return new WaitForSeconds(disappearDelay);
        platformRenderer.enabled = false; 
        platformCollider.enabled = false; 

        Debug.Log("Plataforma desaparecida.");

        // Espera antes de reaparecer
        yield return new WaitForSeconds(reappearDelay);
        platformRenderer.enabled = true;  
        platformCollider.enabled = true;  
        Debug.Log("Plataforma reaparecida.");
    }
}
