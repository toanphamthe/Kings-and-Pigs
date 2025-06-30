using System;

public interface IPlayerHealth
{
    int CurrentHealth { get; set; }
    int MaxHealth { get; set; }

    event Action OnHealthChanged;

    void IncreaseHealth(int amount);
    void DecreaseHealth(int amount);
    void RestoreHealth();
}