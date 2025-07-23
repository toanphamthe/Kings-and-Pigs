using System.Collections;
using UnityEngine;

public class EnemyHit : MonoBehaviour, IEnemyHit
{
    [SerializeField] protected int _health;
    [SerializeField] protected bool _isStunned;
    [SerializeField] protected float _knockbackForce;
    [SerializeField] protected float _knockbackDuration;

    // Applies damage to the enemy and initiates knockback.
    public virtual void TakeDamage(int damage, Vector2 attackerPosition)
    {
        if (_isStunned) return;
        _health -= damage;
        StartCoroutine(ApplyKnockback(attackerPosition));
    }

    public virtual int GetHealth()
    {
        return _health;
    }

    // Checks if the player is currently stunned.
    public virtual bool IsStunned()
    {
        return _isStunned;
    }

    // Applies knockback to the player based on the attacker's position.
    protected virtual IEnumerator ApplyKnockback(Vector2 attackerPosition)
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
