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
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            if (availableSpawnPoints.Count == 0)
            {
                // Si no hay más puntos de spawn disponibles, romper el bucle
                Debug.LogWarning("No hay suficientes puntos de spawn para el número de enemigos solicitados.");
                break;
            }

            // Seleccionar un punto de spawn aleatorio y removerlo de la lista disponible
            int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[spawnIndex];
            availableSpawnPoints.RemoveAt(spawnIndex);

            // Seleccionar un prefab de enemigo aleatorio
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            // Instanciar el enemigo en el punto de spawn seleccionado
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Esperar antes de spawnear el próximo enemigo
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
