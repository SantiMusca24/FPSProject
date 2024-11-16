using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : EnemyHealthClass
{
    private void Awake()
    {
        health = 50;
        debugTest = "Red";
    }
}
