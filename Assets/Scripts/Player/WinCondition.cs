using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public int totalEnemies = 2; 
    private int currentEnemiesDead = 0; 

#if UNITY_EDITOR
    public SceneAsset sceneToLoad;  
#endif

    private string sceneName; 

    void Start()
    {
        
#if UNITY_EDITOR
        if (sceneToLoad != null)
        {
            sceneName = sceneToLoad.name;
        }
#endif
    }

    
    public void EnemyDied()
    {
        currentEnemiesDead++;

        if (currentEnemiesDead >= totalEnemies)
        {
            
            SceneManager.LoadScene(sceneName);
        }
    }
}
