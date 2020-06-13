using TowerDefence.Units.Common;
using UnityEngine;

namespace TowerDefence.Units.Enemy
{
    public interface IEnemy:IKillable
    {
        int Damage { get; }
        void Init(EnemyConfig config, Vector3 initialPosition);
    }
}