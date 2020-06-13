using System;
namespace TowerDefence.Common.Health
{
    public interface IHealthComponent
    {
        Action<float> OnHealthPercentageChanged { get; set; }

        int GetCurrentHealth();
        void SetHealth(int amount);
        void UpdateHealth(int amount);
    }
}