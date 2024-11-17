using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 - Manuel Pereiro
public class EnemyRed : EnemyHealthClass
{
    public GameObject redAudio;
    protected override void Awake()
    {
        base.Awake();
        gruntz = redAudio.GetComponent<AudioSource>();
        health = 50;
        halfHealth = 25;
        debugTest = "Red";
    }
    protected override void HalfHealth()
    {
        gruntz.Play();
        health = 50;


    }


}
