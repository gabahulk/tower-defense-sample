using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Enemy
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField]
        private Transform Destination = default;
        [SerializeField]
        private NavMeshAgent NavMeshAgent = default;
        private IMovementComponent movementComponent;

        [SerializeField]
        private IHealthComponent healthComponent;
        [SerializeField]
        private HealthBar HealthBar = default;

        private void Awake()
        {
            //debug while there is no spawner
            var config = ScriptableObject.CreateInstance<EnemyConfig>();
            config.MaxHealth = 10;
            Init(config);
        }

        public void Init(EnemyConfig config)
        {
            movementComponent = new MovementComponent(NavMeshAgent, Destination);
            healthComponent = new HealthComponent(config.MaxHealth);
            healthComponent.OnHealthPercentageChanged += OnHealthChanged;

            movementComponent.SetDestination();
        }

        public void Update()
        {
            if (Input.anyKeyDown)
            {
                healthComponent.UpdateHealth(-1);
            }
        }

        private void OnHealthChanged(float healthPercentage)
        {
            if (healthPercentage <= 0)
            {
                Destroy(gameObject);
            }
            HealthBar.SetHealthBarFillAmount(healthPercentage);
        }

        private void OnDestroy()
        {
            healthComponent.OnHealthPercentageChanged -= OnHealthChanged;
        }
    }
}
