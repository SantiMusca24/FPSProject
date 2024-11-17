using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScoreManager : MonoBehaviour
{
    public enum EnemyType
    {
        Melee,
        Flying
    }
    private Dictionary<EnemyType, int> enemyScores = new Dictionary<EnemyType, int>();
    public void Awake()
    {
        enemyScores.Add(EnemyType.Melee, 100);
        enemyScores.Add(EnemyType.Flying, 200);
    }


}
