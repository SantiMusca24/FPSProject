using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class key : MonoBehaviour, IInteractuable
{
    // Lorenzo Marmol
    public GameObject doorKey;          
    public GameObject ColiderDoor;     
    public TextMeshProUGUI interactText; 
    private bool isPlayerNearby = false; 
    public void Start()
    {
        interactText.gameObject.SetActive(false);
    }
    void Update()
    {
        
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactText.gameObject.SetActive(true); 
            Debug.Log("colision");
        }
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactText.gameObject.SetActive(false); 
        }
    }

    public void Interact()
    {
        ColiderDoor.gameObject.SetActive(true); 
        Destroy(doorKey);                       
        interactText.gameObject.SetActive(false); 
    }
}
