using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;
    [SerializeField]
    private GameObject player;
    private float firstSpawnTime = 1.5f;
    private float spawnInterval = 3f;
    private float spawnIntervalDecrement = 0.1f;
    private int spawnPerInterval = 1;
    private float hellTime = 180f;

    private float currentSpawnTime;
    private float currentSpawnInterval;
    private float gameTime;
    
    
    private float spawnMinDistance = 2f;
    private float spawnMaxDistance = 5f;

    private void Start()
    {
        currentSpawnTime = firstSpawnTime;
        currentSpawnInterval = spawnInterval;
        gameTime = 0f;
        Invoke("SpawnEnemies", currentSpawnTime);
    }
    private void Update()
    {
        gameTime += Time.deltaTime;
    }
    private void SpawnEnemies()
    {
        spawnPerInterval = Mathf.Clamp(Mathf.FloorToInt(gameTime / 20) + 1,1,9);

        for (int i = 0; i < spawnPerInterval;i++)
        {
            Vector2 spawnpos = player.transform.position;
            Vector2 offset = new Vector2(UnityEngine.Random.Range(spawnMinDistance, spawnMaxDistance), UnityEngine.Random.Range(spawnMinDistance, spawnMaxDistance));
            offset.x= UnityEngine.Random.Range(0, 2) == 1 ? offset.x : -offset.x;
            offset.y= UnityEngine.Random.Range(0, 2) == 1 ? offset.y : -offset.y;
            spawnpos+=offset;

            Instantiate(zombie, spawnpos, Quaternion.identity);
        }
        currentSpawnInterval -= spawnIntervalDecrement*Mathf.Clamp01(gameTime/hellTime);
        currentSpawnTime=Mathf.Max(currentSpawnInterval,0.2f);
        Invoke("SpawnEnemies", currentSpawnTime);
    }
}
