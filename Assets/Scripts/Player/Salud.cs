using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class Salud : MonoBehaviour
{
    //Lorenzo Marmol
    public static float health = 100f;
    public float maxHealth = 100f;

    [Header("Interfaz")]
    public Image healthBar;
    public Text healthText;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        // Actualizar la barra de salud
        CheckHealth();

        // Cambiar a la escena de muerte si la salud llega a 0
        if (health <= 0)
        {
            SceneManager.LoadScene("Death_Scene");
        }
    }

    private void CheckHealth()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = health.ToString("F0");
    }
}
