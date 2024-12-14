using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    private void OnEnable()
    {
        Salud.OnDamageReceived += HandleDamage;
    }

    private void OnDisable()
    {
        Salud.OnDamageReceived -= HandleDamage;
    }

    private void HandleDamage(float damage)
    {
        Debug.Log("El jugador recibió " + damage + " puntos de daño.");
        
    }
}
