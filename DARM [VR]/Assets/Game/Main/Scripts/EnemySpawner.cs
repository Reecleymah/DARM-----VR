using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Lista de prefabs de enemigos para spawnear
    public List<GameObject> enemyPrefabs;
    public Transform[] spawnPoints;
    public int numberOfEnemies = 5;
    public float spawnDelay = 1f;

    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerInRange)
        {
            playerInRange = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
