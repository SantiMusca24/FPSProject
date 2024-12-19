using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    //Lorenzo Marmol
    public bool invencible = false;
    public GameObject infectionIcon;
    public GameObject VenenoFiltro;
    public float invencibleTime = 1f;
    public int infectionDamage = 10;
    static public bool maskOn = false;

    private void OnTriggerEnter(Collider Player)
    {
        VenenoFiltro.SetActive(true);
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
        VenenoFiltro.SetActive(true);
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
        VenenoFiltro.SetActive(false);
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
            VenenoFiltro.SetActive(false);
        }
        
    }

}