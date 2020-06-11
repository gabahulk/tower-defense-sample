using System;
namespace TowerDefence.Common.Health
{
    public class HealthComponent : IHealthComponent
    {
        private int maxHealth;
        private int currentHealth;

        public Action<float> OnHealthPercentageChanged { get; set; }

        public HealthComponent(int maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        public void UpdateHealth(int amount)
        {
            if (amount == 0)
                return;

            currentHealth += amount;
            float healthPercentage = (float)currentHealth / (float)maxHealth;
            OnHealthPercentageChanged?.Invoke(healthPercentage);
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }
    }
}
