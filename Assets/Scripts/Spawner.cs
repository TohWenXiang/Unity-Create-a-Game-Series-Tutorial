using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;

    Wave currentWave;
    int currentWaveNumber;
    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        //as long as there is one enemy not spawned and time is more than next spawn time
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            //amount of enemy left to spawn is decreased
            enemiesRemainingToSpawn--;
            //update timer
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            //create enemy
            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity, transform);
            //accept broadcast from enemy about when it has died
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        Debug.Log("Wave: " + currentWaveNumber);
        //allow next wave to happen if current wave is not the last
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
        }
    }

    void OnEnemyDeath()
    {
        Debug.Log("An enemy is dead");
        //amount of enemy left alive is decreased
        enemiesRemainingAlive--;

        //advance to next wave when all enemy in this wave has died 
        if (enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }
}
