using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlue : EnemyHealthClass
{
    private void Awake()
    {
        health = 100;
        debugTest = "Blue";
    }
}
