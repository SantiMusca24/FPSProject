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

    
    public delegate void DamageEventHandler(float damage);
    public static event DamageEventHandler OnDamageReceived;

    private void Start()
    {
        health = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        
        if (health <= 0)
        {
            SceneManager.LoadScene("Death_Scene");
        }
    }

    
    public void ReceiveDamage(float damage)
    {
        if (damage <= 0) return;

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth); 

        
        UpdateHealthUI();

        
        OnDamageReceived?.Invoke(damage);
    }

    
    private void UpdateHealthUI()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = health.ToString("F0");
    }
}
