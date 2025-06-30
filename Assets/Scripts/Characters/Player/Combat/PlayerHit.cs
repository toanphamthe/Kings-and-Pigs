using System;
using System.Collections;
using UnityEngine;

public class PlayerHit : MonoBehaviour, IPlayerDamageable
{
    [SerializeField] private PlayerHealthPresenter _playerHealthPresenter;
    [SerializeField] private bool _isStunned;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _knockbackDuration;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<IEnemyAttack>();
            if (enemy != null)
            {
                TakeDamage(1, collision.transform.position);
            }
        }
    }

    // Applies damage to the player and initiates knockback.
    public void TakeDamage(int damage, Vector2 attackerPosition)
    {
        if (_isStunned) return;
        _playerHealthPresenter = GameObject.Find("Player Health Presenter")?.GetComponent<PlayerHealthPresenter>();
        if (_playerHealthPresenter != null)
        {
            _playerHealthPresenter.Damage(1);
        }
        StartCoroutine(ApplyKnockback(attackerPosition));
    }

    // Checks if the player is currently stunned.
    public bool IsStunned()
    {
        return _isStunned;
    }

    // Applies knockback to the player based on the attacker's position.
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
