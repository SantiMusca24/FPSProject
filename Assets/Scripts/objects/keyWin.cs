using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class keyWin : MonoBehaviour, IInteractuable
{
    // Lorenzo Marmol
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
        SceneManager.LoadScene("Win_Scene"); 
    }
}
