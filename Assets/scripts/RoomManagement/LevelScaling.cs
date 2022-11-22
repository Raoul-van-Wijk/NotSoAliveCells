using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelScaling : MonoBehaviour
{
    public float runTime;
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private List<GameObject> enemySpawnPoints;

    private float currentDifficulty = 2f;
    public int minEnemies = 4;
    public int maxEnemies = 8;

    private int enemyCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies(minEnemies, maxEnemies);
    }


    /// <summary>
    /// Spawns enemies at random locations
    /// </summary>
    /// <param name="min">Min amount of enemies that needs to be spawned</param>
    /// <param name="max">Max amount of enemies that needs to be spawned</param>

    private void SpawnEnemies(int min, int max)
    {
        if (enemySpawnPoints.Count == 0 || max == 0) return;
        for (float i = Random.Range(min, max +1); i > 0; i--)   
        {
            int random = Random.Range(0, enemySpawnPoints.Count - 1);
            enemySpawnPoints.RemoveAt(random);
            GameObject instance = Instantiate(enemyObject, enemySpawnPoints[random].transform.position, Quaternion.identity);
            instance.transform.SetParent(this.transform);
            instance.gameObject.GetComponent<EnemyTestScript>().IncreaseStrengthByDifficulty(currentDifficulty);
            enemyCount++;    
        }
    }



    private void OnDrawGizmos()
    {
        foreach (GameObject spawn in enemySpawnPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawn.transform.position, 0.5f);
        }
    }
}
