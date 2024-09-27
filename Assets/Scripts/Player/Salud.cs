using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class Salud : MonoBehaviour
{
    static public float health = 100;
    public float maxHealth = 100;

    [Header("Interfaz")]
    public Image healthBar;
    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();

        if (health <= 0)
        {

            SceneManager.LoadScene("Death_Scene");
        }

    }
    void CheckHealth()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = health.ToString("f0");
    }
}
