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

    [SerializeField] private bool noAcclearationEnemey = false;

    [SerializeField] private float AcclearationMutiple = 0;
    [SerializeField] private float AccleartionIncreaseStart = 0.1f;
    [SerializeField] private float AccleartionIncreasePostStart = 0.02f;
    bool firstEnemy = true;

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
        GameObject enemy = Instantiate(randomEnemy.Prefab, position, Quaternion.identity);
        if (noAcclearationEnemey) // Wahrscheinlich nicht ideal aber für jetzt erstmal ok.
        {
            enemy.GetComponent<EnemyMovement>()?.SetaccelerationValue(0); 
        }
        if (AcclearationMutiple < 1)
        {
            AcclearationMutiple += AccleartionIncreaseStart;
            enemy.GetComponent<EnemyMovement>()?.SetaccelerationValueMutiple(AcclearationMutiple);
        }
        else
        {
            AcclearationMutiple += AccleartionIncreasePostStart;
            enemy.GetComponent<EnemyMovement>()?.SetaccelerationValueMutiple(AcclearationMutiple);
        }
       

        Debug.Log("<color=" + randomEnemy.Name + ">●</color> Chance: <b>" + randomEnemy.Chance + "</b>%");
    }

    private Enemy GetRandomEnemy()
    {
        if (firstEnemy)
        {
            firstEnemy = false;
            return enemies[0];
        }
        else
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
}
