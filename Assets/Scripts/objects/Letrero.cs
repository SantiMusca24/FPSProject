using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Letrero : MonoBehaviour
{
    public TextMeshProUGUI interactText;
    private bool isPlayerNearby = false;
    public void Start()
    {
        interactText.gameObject.SetActive(false);
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
}
