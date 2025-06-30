using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IPlayerHealth
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public event Action OnHealthChanged; // This event is triggered whenever the health changes

    // Increases the player's health by a specified amount, clamping it to the maximum health
    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        UpdateHealth();
    }

    // Decreases the player's health by a specified amount, clamping it to a minimum of 0
    public void DecreaseHealth(int amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        UpdateHealth();
    }

    // Restores the player's health to the maximum health value
    public void RestoreHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealth();
    }

    // This method is called to update the health and trigger the OnHealthChanged event
    public void UpdateHealth()
    {
        OnHealthChanged?.Invoke();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
}
