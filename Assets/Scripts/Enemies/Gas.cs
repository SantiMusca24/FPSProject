using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    public bool invencible = false;
    public GameObject infectionIcon;
    public float invencibleTime = 1f;
    public int infectionDamage = 10;
    static public bool maskOn = false;

    private void OnTriggerEnter(Collider Player)
    {
        if (maskOn == false && !invencible && Salud.health > 0)
        {
            Salud.health -= infectionDamage;
            infectionIcon.SetActive(true);
            print("te dio");
            StartCoroutine(invulnerability());
        
        }
       
    }
    private void OnTriggerStay(Collider Player)
    {
        if (maskOn == false && !invencible && Salud.health > 0)
        {
            Salud.health -= infectionDamage;
            infectionIcon.SetActive(true);
            print("te dio");
            StartCoroutine(invulnerability());
          
        }

    }
    private void OnTriggerExit(Collider Player)
    {
        infectionIcon.SetActive(false);
    }
    IEnumerator invulnerability()
    {
        invencible = true;
        yield return new WaitForSeconds(invencibleTime);
        invencible = false;
    }

    private void Update()
    {
        if (maskOn == true)
        {
            infectionIcon.SetActive(false);
        }
    }
}