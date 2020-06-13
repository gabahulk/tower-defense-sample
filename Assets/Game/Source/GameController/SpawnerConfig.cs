using System.Collections.Generic;
using TowerDefence.Units.Enemy;
using UnityEngine;

namespace TowerDefence.GameController
{
    [CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Game/SpawnerConfig")]
    public class SpawnerConfig : ScriptableObject
    {
        public float DelayBetweenWavesInSeconds;
        public float DelayBetweenSpawnsInSeconds;
        public float InitialDelayInSeconds;
        public List<EnemyConfig> Enemies;
        public Vector2Int WaveSizeRange;
        public int NumberOfWaves;
    }
}
