using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementBehaviour : MonoBehaviour
{
    public Transform Destination;
    public NavMeshAgent NavMeshAgent;

    void Start()
    {
        SetDestination();
    }

    private void SetDestination()
    {
        if (Destination != null)
        {
            NavMeshAgent.SetDestination(Destination.transform.position);
        }
    }
}
