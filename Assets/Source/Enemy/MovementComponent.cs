using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Enemy
{
    public class MovementComponent : IMovementComponent
    {
        private NavMeshAgent agent;
        private Transform destination;

        public MovementComponent(NavMeshAgent agent, Transform destination)
        {
            this.agent = agent;
            this.destination = destination;
        }

        public void SetDestination()
        {
            if (destination != null)
            {
                agent.SetDestination(destination.transform.position);
            }
        }
    }
}
