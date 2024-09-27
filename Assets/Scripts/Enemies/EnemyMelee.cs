using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public int enemyRoutine;
    public float timer;
    public Quaternion angle;
    public float axis;

    public GameObject target;
    public bool isAttack;

    Salud salud;
    [SerializeField] GameObject player;
    public Material colorEngage;
    


    private void Awake()
    {
        salud = player.GetComponent<Salud>();
    }

    public void EnemyBehaviour ()
    {

        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            colorEngage.color = Color.green;
            timer += 1 * Time.deltaTime;

            if (timer >=4)
            {
            enemyRoutine = Random.Range(0, 2);
            timer = 0;
            }
            
            switch (enemyRoutine)
	        {
	        case 0:

                break;

            case 1:
                    axis = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, axis, 0);
                        enemyRoutine++;
                break;

            case 2:
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                        transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                break;
	        }

        }
        else
        {

            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !isAttack)
            {
                colorEngage.color = Color.red;
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            }
            else
            {
                isAttack = true;
            }
        }

    }

    public void atkFinished()
    {
        isAttack = false;
    }



    private void Update()
    {
        EnemyBehaviour();

    }
   /* public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 30)
        {
            Salud.health -= 1f;
        }
    }
   */
}
