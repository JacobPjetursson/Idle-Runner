using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPopulator : Singleton<MapPopulator>
{
    // list of different patterns of coins and enemies that may appear on the screen
    public GameObject[] coinPatterns;
    public GameObject[] enemyPatterns;
    // min and max distance the player travels before a new pattern appears
    public float minCoinDistance = 10f;
    public float maxCoinDistance = 30f;

    public float minEnemyDistance = 10f;
    public float maxEnemyDistance = 30f;

    private float coinDistance, enemyDistance; // these are randomized based on above interval
    private float lastCoinPos, lastEnemyPos;
    private GameObject coins, enemies; // folders for the objects
    void Start()
    {
        coins = new GameObject("coins");
        enemies = new GameObject("enemies");
        // starter values
        lastCoinPos = GameManager.Instance.GetTotalDistanceTraveled();
        lastEnemyPos = GameManager.Instance.GetTotalDistanceTraveled();
        coinDistance = Random.Range(minCoinDistance, maxCoinDistance);
        enemyDistance = Random.Range(minEnemyDistance, maxEnemyDistance);
    }


    void FixedUpdate()
    {
        float totalDist = GameManager.Instance.GetTotalDistanceTraveled();
        float currCoinDistance = (totalDist - lastCoinPos);
        if (currCoinDistance > coinDistance)
        {
            // randomize coinDistance
            coinDistance = Random.Range(minCoinDistance, maxCoinDistance);
            // set coin pos
            lastCoinPos = totalDist;
            // spawn coin pattern at random
            Spawn(coinPatterns);
        }
        float currEnemyDistance = (totalDist - lastEnemyPos);
        if (currEnemyDistance > enemyDistance)
        {
            // randomize enemyDistance
            enemyDistance = Random.Range(minEnemyDistance, maxEnemyDistance);
            // set enemy pos
            lastEnemyPos = totalDist;
            // spawn enemy pattern at random
            Spawn(enemyPatterns);
        }

    }

    private void Spawn(GameObject[] patterns)
    {
        // spawn one of the patterns at random
        int index = Random.Range(0, patterns.Length);
        GameObject instance = Instantiate(patterns[index]);
    }
}
