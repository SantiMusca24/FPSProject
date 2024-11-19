using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Lorenzo Marmol
    public string animationName;
    public Animator animator;
    public GameObject doorWall;

    private void OnTriggerEnter(Collider Player)
    {
        animator.Play(animationName);
        doorWall.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
