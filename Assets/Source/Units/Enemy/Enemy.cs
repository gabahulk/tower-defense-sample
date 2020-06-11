using TowerDefence.Common.Health;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Units.Enemy
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

        public int Damage { get; private set; }

        private void Awake()
        {
            //debug while there is no spawner
            var config = ScriptableObject.CreateInstance<EnemyConfig>();
            config.MaxHealth = 10;
            config.Damage = 5;
            Init(config);
        }

        public void Init(EnemyConfig config)
        {
            healthComponent = new HealthComponent(config.MaxHealth);
            healthComponent.OnHealthPercentageChanged += HealthBar.OnHealthChanged;

            movementComponent = new MovementComponent(NavMeshAgent, Destination);
            movementComponent.SetDestination();

            Damage = config.Damage;
        }

        public void TakeDamage(int damage)
        {
            healthComponent.UpdateHealth(-damage);
            if (healthComponent.GetCurrentHealth() <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            healthComponent.OnHealthPercentageChanged -= HealthBar.OnHealthChanged;
        }
    }
}
