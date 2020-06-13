using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Units.Enemy
{
    public class MovementComponent : IMovementComponent
    {
        private NavMeshAgent agent;
        private Transform destination;

        public MovementComponent(NavMeshAgent agent, Transform destination, float speed, float acceleration)
        {
            this.agent = agent;
            this.destination = destination;
            agent.acceleration = acceleration;
            agent.speed = speed;
        }

        public void SetDestination()
        {
            if (destination != null)
            {
                agent.isStopped = false;
                agent.SetDestination(destination.transform.position);
            }
          
        }

        public void SetPosition(Vector3 position)
        {
            agent.Warp(position);
        }
    }
}
