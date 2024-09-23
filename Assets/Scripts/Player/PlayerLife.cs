using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    EnemyView enemyView;
    [SerializeField] GameObject enemy;

    [SerializeField] float _playerMaxLife = 10000f;
    float _playerCurrentLife;
    [SerializeField] float _atkCD;
    float _chosenAtkCD;
    private void Awake()
    {
        enemyView = enemy.GetComponent<EnemyView>();
        _chosenAtkCD = _atkCD;
        _playerCurrentLife = _playerMaxLife;
    }

    void FixedUpdate()
    {
        enemyView.hitProbability = Random.Range(1, 11);

        if (_atkCD <=0)
        {
            _atkCD = _chosenAtkCD;

            if (enemyView.canSeePlayer == true)
            {    

                if (enemyView.hitProbability >=4)    
                {    

                    _playerCurrentLife -= enemyView.laserDmg;
                    Debug.Log(enemyView.laserDmg + " de daño recibido, " + _playerCurrentLife + " de vida restante");    
                    _atkCD = _chosenAtkCD;         

                        if (_playerCurrentLife <= 0)
                        {
                            Destroy(this.gameObject);
                            Debug.Log("Jugador murió");
                        }
                }

                else
                {    
                    Debug.Log("Tiro esquivado");
                }    

            }    

        }

        else
        {
            _atkCD--;
            Debug.Log("Remaining CD: " + _atkCD);
        }

    }

}
