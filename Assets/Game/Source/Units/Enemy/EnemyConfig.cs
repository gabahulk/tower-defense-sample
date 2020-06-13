using UnityEngine;

namespace TowerDefence.Units.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Game/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public int MaxHealth;
        public int Damage;
        public Color color;
        public float Speed;
        public float Acceleration;
    }
}
