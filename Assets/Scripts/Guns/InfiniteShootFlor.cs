using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfiniteShootFlor : MonoBehaviour, IInteractuable
{
    public GameObject m4;
    public TextMeshProUGUI interactText;
    public TextMeshPro puntos;
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

    public void Interact()
    {
        GunClass._infShot = true;
        Debug.Log("activo2");
        m4.SetActive(false);
        interactText.gameObject.SetActive(false);
        puntos.gameObject.SetActive(false);
    }
}
