using System;
using TowerDefence.Common.Health;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Units.Enemy
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private Transform Destination = default;
        [SerializeField]
        private NavMeshAgent NavMeshAgent = default;
        private IMovementComponent movementComponent;

        private IHealthComponent healthComponent;
        [SerializeField]
        private HealthBar HealthBar = default;

        public MeshRenderer meshRenderer;
        public int Damage { get; private set; }
        public Action<GameObject> OnDeathEvent { get; set; }

        public void Init(EnemyConfig config, Vector3 initialPosition)
        {
            Destination = GameObject.FindGameObjectWithTag("Throne").transform;
            if (healthComponent == null)
            {
                healthComponent = new HealthComponent(config.MaxHealth);
                healthComponent.OnHealthPercentageChanged += HealthBar.OnHealthChanged;
            }
            healthComponent.SetHealth(config.MaxHealth);

            if (movementComponent == null)
            {
                movementComponent = new MovementComponent(NavMeshAgent, Destination, config.Speed, config.Acceleration);
            }
            movementComponent.SetPosition(initialPosition);
            movementComponent.SetDestination();

            Damage = config.Damage;

            meshRenderer.material.SetColor("_Color", config.color);
        }

        public void TakeDamage(int damage)
        {
            healthComponent.UpdateHealth(-damage);
            if (healthComponent.GetCurrentHealth() <= 0)
            {
                OnDeathEvent?.Invoke(gameObject); 
            }
        }

        private void OnDestroy()
        {
            if (healthComponent != null)
            {
                healthComponent.OnHealthPercentageChanged -= HealthBar.OnHealthChanged;
            }
        }
    }
}
