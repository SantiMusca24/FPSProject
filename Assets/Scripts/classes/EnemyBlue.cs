using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TP2 - Manuel Pereiro
public class EnemyBlue : EnemyHealthClass
{
    public GameObject blueAudio;
    protected override void Awake()
    {
        base.Awake();
        gruntz = blueAudio.GetComponent<AudioSource>();
        health = 100;
        halfHealth = 50;
        debugTest = "Blue";
    }

    protected override void HalfHealth()
    {
        gruntz.Play();
    }
}
