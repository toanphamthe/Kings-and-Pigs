using UnityEngine;

public class KingPig : Enemy
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    protected override void Awake()
    {
        base.Awake();
        _currentHealth = _maxHealth;
    }
    public override void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
