using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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

    }

    // Update is called once per frame
    void Update()
    {
        refreshBar();
    }
        void refreshBar ()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = "salud: " + health.ToString("f0");
    }
}
