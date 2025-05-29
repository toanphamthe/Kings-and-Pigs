using System;
using System.Collections;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private bool _isStunned;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _knockbackDuration;


    private void Awake()
    {
        if (_playerHealth == null)
        {
            _playerHealth = GetComponent<PlayerHealth>();
        }
    }

    public void TakeDamage(int damage, Vector2 attackerPosition)
    {
        if (_isStunned) return;
        _playerHealth.CurrentHealth -= damage;
        StartCoroutine(ApplyKnockback(attackerPosition));
    }

    public bool IsStunned()
    {
        return _isStunned;
    }

    private IEnumerator ApplyKnockback(Vector2 attackerPosition)
    {
        Vector2 knockbackDirection = (transform.position - (Vector3)attackerPosition).normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(knockbackDirection * _knockbackForce, ForceMode2D.Impulse);
        }

        _isStunned = true;

        yield return new WaitForSeconds(_knockbackDuration);

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        _isStunned = false;
    }
}
