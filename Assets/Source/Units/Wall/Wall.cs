using TowerDefence.Common.Health;
using TowerDefence.Units.Enemy;
using UnityEngine;

namespace TowerDefence.Units.Wall
{ 
    public class Wall : MonoBehaviour
    {
        [SerializeField]
        private IHealthComponent healthComponent;
        [SerializeField]
        private HealthBar HealthBar = default;

        private void Awake()
        {
            //debug while there is no spawner
            var config = ScriptableObject.CreateInstance<WallConfig>();
            config.MaxHealth = 4;
            Init(config);
        }

        public void Init(WallConfig config)
        {
            healthComponent = new HealthComponent(config.MaxHealth);
            healthComponent.OnHealthPercentageChanged += HealthBar.OnHealthChanged;
        }

        private void TakeDamage(int damage)
        {
            healthComponent.UpdateHealth(-damage);
            if (healthComponent.GetCurrentHealth() <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                var enemy = collision.gameObject.GetComponent<IEnemy>();
                TakeDamage(enemy.Damage);
                Destroy(collision.gameObject);
            }
        }

        private void OnDestroy()
        {
            healthComponent.OnHealthPercentageChanged -= HealthBar.OnHealthChanged;
        }
    }
}
