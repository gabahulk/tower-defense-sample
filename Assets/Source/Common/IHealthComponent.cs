using System;

public interface IHealthComponent
{
    Action<float> OnHealthPercentageChanged { get; set; }

    int GetCurrentHealth();
    void UpdateHealth(int amount);
}