using UnityEngine;

namespace TowerDefence.Units.Enemy
{
    public interface IMovementComponent
    {
        void SetDestination();
        void SetPosition(Vector3 position);
    }
}