using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    EnemyView enemyView;
    [SerializeField] GameObject enemy;

    [SerializeField] float _playerMaxLife = 10000f;
    float _playerCurrentLife;

    private void Awake()
    {
        enemyView = enemy.GetComponent<EnemyView>();
    }

    private void Start()
    {
        _playerCurrentLife = _playerMaxLife;
    }

    void FixedUpdate()
    {
        if (enemyView.canSeePlayer == true)
        {
            _playerCurrentLife -= enemyView.laserDmg;
            Debug.Log(enemyView.laserDmg + " de daño recibido, " + _playerCurrentLife + " de vida restante");
            
            if (_playerCurrentLife <= 0)
            {
                Destroy (this.gameObject);
            }
        }
        else
        {

        }

    }
}
