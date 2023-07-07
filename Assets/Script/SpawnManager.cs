using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabtoSpawn;

    [SerializeField]
    private GameObject EnemyContain;

    [SerializeField]
    private GameObject[] powerUps;

    [SerializeField]
    private bool shouldSpawn = true;

    [SerializeField]
    private GameObject asteroidPrefab;
    
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPower());
        StartCoroutine(SpawnAsteroid());
    }

    IEnumerator SpawnRoutine()
    {
        while (shouldSpawn) 
        {
            Vector3 spawnPos = new Vector3(Random.Range(-10, 10), 7, 0);
            GameObject Enemy = Instantiate(prefabtoSpawn, spawnPos, Quaternion.identity);
            Enemy.transform.SetParent(EnemyContain.transform);
            yield return new WaitForSeconds(Random.Range(1f,5f));
        }
    }

    public void SetShouldSpawn(bool isSpawn)
    {
        this.shouldSpawn = isSpawn;
    }

    IEnumerator SpawnPower()
    {
        while (shouldSpawn) 
        {
            Vector3 spawnPos = new Vector3(Random.Range(-10, 10), 7, 0);
            GameObject Power = Instantiate(powerUps[Random.Range(0, powerUps.Length)], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(15, 20));
        }
    }

    IEnumerator SpawnAsteroid()
    {
        while(shouldSpawn)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-10, 10), 7, 0);
            GameObject Power = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4f, 10f));
        }
    }
}
