using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpike : MonoBehaviour
{
    public bool invencible = false;
    public bool poisoned = false;
    public GameObject poisonIcon;
    public int poisonedTime = 5;
    public int poisonDamage = 1;
    public float invencibleTime = 1f;
    public int spikeDamage = 10;

    private void OnTriggerEnter(Collider Player)
    {
        if (!invencible && Salud.health > 0)
        {
            Salud.health -= spikeDamage;
            print("te dio");
            StartCoroutine(invulnerability());
            StartCoroutine(poison());
            StartCoroutine(poisonTime());
        }

    }
    private void OnTriggerStay(Collider Player)
    {
        if (!invencible && Salud.health > 0)
        {
            Salud.health -= spikeDamage;
            print("te dio");
            StartCoroutine(invulnerability());
            StartCoroutine(poison());
            StartCoroutine(poisonTime());
        }

    }
    IEnumerator invulnerability()
    {
        invencible = true;
        yield return new WaitForSeconds(invencibleTime);
        invencible = false;
    }
    IEnumerator poison()
    {
        poisoned = true;
        poisonIcon.SetActive(true);
        yield return new WaitForSeconds(poisonedTime);
        poisonIcon.SetActive(false);
        poisoned = false;
    }
    IEnumerator poisonTime()
    {
     
        while (poisoned == true)
        {
            Salud.health -= poisonDamage;
            yield return new WaitForSeconds(1);
        }
      
    }
    private void Update()
    {
       
    }

}
    

