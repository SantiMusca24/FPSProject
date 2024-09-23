using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{

    [SerializeField] float _enemyMaxLife;
    [SerializeField] int _enemyMaxAmmo;
    public int enemyCurrentLife;
    public int enemyCurrentAmmo;
    public int reloadTime = 3;




    void Start()
    {
        
    }

    
}
