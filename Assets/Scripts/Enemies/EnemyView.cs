using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    //
    //StaticEnemy staticEnemy;
    public GameObject player;
    public float radius;
    [Range(0,360)]
    public float angle;

    public float laserDmg;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public Transform target;

    public bool canSeePlayer;

    public Material hitCheck;
    public Color color1;
    public Color color2;
    public int hitProbability = Random.Range(1,11);

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }

    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                     canSeePlayer = true;
                else 
                     canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void FixedUpdate()
    {
        if (canSeePlayer == true)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
            hitCheck.color = color1;
            

            IEnumerator DmgTimer()
            {
                WaitForSeconds wait = new WaitForSeconds(0.2f);

                while (true)
                {
                    yield return wait;
                    DmgTimer();
                }

            }



        }
        else
        {
            hitCheck.color = color2;
        }

    }



}
