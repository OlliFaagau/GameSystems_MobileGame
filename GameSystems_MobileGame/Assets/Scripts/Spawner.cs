using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawners;
    public GameObject blockPrefab;
    public float timeBetweenWaves = 1f;

    private float timeToSpawn = 2f;
    void Update()
    {
        if (Time.time >= timeToSpawn)
        {
            SpawnRows();
            timeToSpawn = Time.time + timeBetweenWaves;
        }
    }

    void SpawnRows()
    {
        int randomIndex = Random.Range(0, spawners.Length);

        for(int i = 0; i < spawners.Length; i++)
        {
            if(randomIndex != i)
            {
                Instantiate(blockPrefab, spawners[i].position, Quaternion.identity);
            }
        }
    }
}
