using System;
using TowerDefence.Common.Health;
using TowerDefence.Units.Common;
using TowerDefence.Units.Enemy;
using UnityEngine;

namespace TowerDefence.Units.Throne
{ 
    public class Throne : MonoBehaviour, IKillable
    {
        public Action<GameObject> OnDeathEvent { get; set; }
        public ThroneConfig Config;

        [SerializeField]
        private IHealthComponent healthComponent;
        [SerializeField]
        private HealthBar HealthBar = default;

        private void Awake()
        {
            Init(Config);
        }

        public void Init(ThroneConfig config)
        {
            healthComponent = new HealthComponent(config.MaxHealth);
            healthComponent.OnHealthPercentageChanged += HealthBar.OnHealthChanged;
        }

        private void TakeDamage(int damage)
        {
            healthComponent.UpdateHealth(-damage);
            if (healthComponent.GetCurrentHealth() <= 0)
            {
                OnDeathEvent?.Invoke(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                var enemy = collision.gameObject.GetComponent<IEnemy>();
                TakeDamage(enemy.Damage);
                enemy.OnDeathEvent?.Invoke(collision.gameObject);
            }
        }

        private void OnDestroy()
        {
            healthComponent.OnHealthPercentageChanged -= HealthBar.OnHealthChanged;
        }
    }
}
