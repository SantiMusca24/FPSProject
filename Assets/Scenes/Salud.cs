using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class Salud : MonoBehaviour
{
    public bool invencible = false;
    public float invencibleTime = 1f;
    static public float health = 100;
    public float maxHealth = 100;

    [Header("interfaz")]
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
        if (health <= 0)
        {
            SceneManager.LoadScene("Death_Scene");
        }
        refreshBar();
    }
        void refreshBar ()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = "salud: " + health.ToString("f0");
    }
}
