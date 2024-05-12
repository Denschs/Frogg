using UnityEngine;
using System.Collections;

[System.Serializable]
public class Enemy
{
    public string Name;
    public GameObject Prefab;
    [Range(0f, 100f)] public float Chance = 100f;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float maxSpawnInterval = 3f;

    private void Start()
    {
        // Starte die Methode zum Spawnen von Feinden
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Endlos Schleife zum Spawnen von Feinden
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            SpawnRandomEnemy(selectedSpawnPoint.position);
        }
    }

    private void SpawnRandomEnemy(Vector2 position)
    {
        Enemy randomEnemy = GetRandomEnemy();
        Instantiate(randomEnemy.Prefab, position, Quaternion.identity);
        Debug.Log("<color=" + randomEnemy.Name + ">●</color> Chance: <b>" + randomEnemy.Chance + "</b>%");
    }

    private Enemy GetRandomEnemy()
    {
        float totalChance = 0f;
        foreach (Enemy enemy in enemies)
        {
            totalChance += enemy.Chance;
        }

        float randomValue = UnityEngine.Random.Range(0f, totalChance);
        float cumulativeChance = 0f;
        foreach (Enemy enemy in enemies)
        {
            cumulativeChance += enemy.Chance;
            if (randomValue <= cumulativeChance)
            {
                return enemy;
            }
        }
        // Fallback, should never reach here
        return enemies[0];
    }
}
