using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeattack : MonoBehaviour
{
    public GameObject knife;
    public bool CanAttack = true;
    public float AttackCooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
       if (Input.GetKeyDown(KeyCode.B))
        {
            if (CanAttack) { 
            }
            Attack();
            }
    }

    public void Attack()
    {
        CanAttack = false;
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }
    // Update is called once per frame
   IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }
}
