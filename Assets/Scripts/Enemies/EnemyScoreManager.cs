using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyScoreManager : MonoBehaviour
{
    //TP2 Santiago Muscatiello(diccionario)
    public static EnemyScoreManager Instance { get; private set; }
     
    public struct LootData
    {
        public int points;
    }
    public enum EnemyType
    {
        Melee,
        Flying
    }
    private Dictionary<EnemyType, LootData> enemyScores = new Dictionary<EnemyType, LootData>();
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Asegura que solo haya una instancia
        }

        
        DontDestroyOnLoad(gameObject);
        enemyScores.Add(EnemyType.Melee, new LootData{points= 100});
        enemyScores.Add(EnemyType.Flying, new LootData { points = 200 });
    }

    public LootData GetPoints(EnemyType enemyType)
    {
        if(enemyScores.TryGetValue(enemyType,out var lootData))
        {
            return lootData;
        }
        return new LootData { points = 0 };
    }

}
