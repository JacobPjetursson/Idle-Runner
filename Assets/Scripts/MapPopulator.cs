using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPopulator : Singleton<MapPopulator>
{
    // list of different patterns of coins and enemies that may appear on the screen
    public GameObject[] coinPatterns;
    public GameObject[] enemyPatterns;
    public GameObject[] boxes;
    public GameObject defaultBox; // if none of the other boxes trigger, this is the default box to spawn

    // min and max distance the player travels before a new pattern appears
    public float minCoinDistance = 10f;
    public float maxCoinDistance = 30f;

    public float minEnemyDistance = 10f;
    public float maxEnemyDistance = 30f;

    public float minBoxDistance = 10f;
    public float maxBoxDistance = 10f;

    private float coinDistance, enemyDistance, boxDistance; // these are randomized based on above interval
    private float lastCoinPos, lastEnemyPos, lastBoxPos;
    void Start()
    {
        // starter values
        lastCoinPos = GameManager.Instance.GetTotalDistanceTraveled();
        lastEnemyPos = GameManager.Instance.GetTotalDistanceTraveled();
        lastBoxPos = GameManager.Instance.GetTotalDistanceTraveled();

        coinDistance = Random.Range(minCoinDistance, maxCoinDistance);
        enemyDistance = Random.Range(minEnemyDistance, maxEnemyDistance);
        boxDistance = Random.Range(minBoxDistance, maxBoxDistance);
    }


    void FixedUpdate()
    {
        float totalDist = GameManager.Instance.GetTotalDistanceTraveled();
        SpawnCoins(totalDist);
        SpawnEnemies(totalDist);
        SpawnBoxes(totalDist);

    }

    private void SpawnCoins(float totalDist)
    {
        float distanceSinceLast = totalDist - lastCoinPos;
        if (distanceSinceLast > coinDistance)
        {
            coinDistance = Random.Range(minCoinDistance, maxCoinDistance);
            lastCoinPos = totalDist;
            SpawnRandom(coinPatterns);
        }
    }

    private void SpawnEnemies(float totalDist)
    {
        float distanceSinceLast = totalDist - lastEnemyPos;
        if (distanceSinceLast > enemyDistance)
        {
            enemyDistance = Random.Range(minEnemyDistance, maxEnemyDistance);
            lastEnemyPos = totalDist;
            SpawnRandom(enemyPatterns);
        }
    }

    private void SpawnBoxes(float totalDist)
    {
        float distanceSinceLast = totalDist - lastBoxPos;
        if (distanceSinceLast > boxDistance)
        {
            boxDistance = Random.Range(minBoxDistance, maxBoxDistance);
            lastBoxPos = totalDist;
            SpawnBox(boxes);
        }
    }

    private void SpawnRandom(GameObject[] patterns)
    {
        // spawn one of the patterns at random
        int index = Random.Range(0, patterns.Length);
        Instantiate(patterns[index], transform);
    }

    private void SpawnBox(GameObject[] boxes)
    {
        float random = Random.value;
        float progress = 0f;
        // loop through all boxes and check if it triggers
        foreach (GameObject boxObj in boxes)
        {
            Box box = boxObj.GetComponent<Box>();
            if (random >= progress && random < (progress + box.triggerChance))
            {
                Instantiate(boxObj, transform);
                return;
            }
            progress += box.triggerChance;
        }
        // if nothing has been triggered, instantiate the default box
        Instantiate(defaultBox, transform);
    }
}
