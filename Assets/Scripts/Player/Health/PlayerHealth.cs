using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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

    public event Action OnHealthChanged;

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        UpdateHealth();
    }

    public void DecreaseHealth(int amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        UpdateHealth();
    }

    public void RestoreHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        OnHealthChanged?.Invoke();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
}
