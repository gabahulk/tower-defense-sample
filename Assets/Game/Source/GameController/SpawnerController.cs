using System.Collections;
using TowerDefence.Common;
using TowerDefence.Units.Enemy;
using UnityEngine;

namespace TowerDefence.GameController
{
    public class SpawnerController : MonoBehaviour
    {
        public GameObject EnemyPrefab;
        public SpawnerConfig config;
        public Transform[] SpawnLocations;
        public System.Action<bool> FinishAllWavesEvent;

        private ObjectPooler pool;
        private int minimumWaveSize;
        private int maximumWaveSize;

        private int enemiesLeft = 0;

        public void Init()
        {
            minimumWaveSize = config.WaveSizeRange.x;
            maximumWaveSize = config.WaveSizeRange.y;
            pool = new ObjectPooler(maximumWaveSize, EnemyPrefab);
            StartCoroutine(nameof(StartSpawning));
        }

        IEnumerator StartSpawning()
        {
            yield return new WaitForSeconds(config.InitialDelayInSeconds);
            int numberOfSpawnedWaves = 0;
            var numberOfEnemies = Random.Range(minimumWaveSize, maximumWaveSize);
            while (numberOfSpawnedWaves <= config.NumberOfWaves)
            {
                if (HasWaveFinished())
                {
                    enemiesLeft = numberOfEnemies;
                    yield return SpawnWave(numberOfEnemies);
                    numberOfSpawnedWaves++;
                }
                else
                {
                    numberOfEnemies = Random.Range(minimumWaveSize, maximumWaveSize);
                }
                yield return new WaitForSeconds(config.DelayBetweenWavesInSeconds);
            }

            while (!HasWaveFinished())
            {
                yield return new WaitForSeconds(config.DelayBetweenWavesInSeconds);
            }
            FinishAllWavesEvent?.Invoke(true);
        }

        private bool HasWaveFinished()
        {
            return enemiesLeft <= 0;
        }

        private void DeactivateEnemy(GameObject enemyGO)
        {
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            enemy.OnDeathEvent -= DeactivateEnemy;
            enemiesLeft--;
            enemyGO.SetActive(false);
        }

        IEnumerator SpawnWave(int totalNumberOfEnemies)
        {
            int numberOfSpawnedEnemies = 0;
            while (numberOfSpawnedEnemies < totalNumberOfEnemies)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(config.DelayBetweenSpawnsInSeconds);
                numberOfSpawnedEnemies++;
            }
        }
        
        private void SpawnEnemy()
        {
            Transform choosenSpawnLocation = SpawnLocations[Random.Range(0, SpawnLocations.Length)];
            GameObject enemyGO = pool.GetObject();

            Enemy enemy = enemyGO.GetComponent<Enemy>();
            enemy.OnDeathEvent += DeactivateEnemy;
            enemy.Init(config.Enemies[Random.Range(0, config.Enemies.Count)], choosenSpawnLocation.position);
        }
    }
}
