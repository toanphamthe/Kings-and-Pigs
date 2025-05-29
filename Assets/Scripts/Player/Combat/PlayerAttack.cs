using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private GameObject _attackHitbox;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _damageAmount;

    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        _attackHitbox = transform.Find("AttackHitBox").gameObject;
        _attackHitbox.SetActive(false);
    }

    public void Attack()
    {
        IsAttacking = true;
        float attackSize = _attackHitbox.gameObject.GetComponent<CircleCollider2D>().radius;
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_attackHitbox.transform.position, attackSize, _enemyLayer);

        foreach (Collider2D enemy in enemiesHit)
        {
            Debug.Log("Hit enemy: " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(_damageAmount);
        }
    }

    public void StopAttack()
    {
        IsAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackHitbox == null) return;
        CircleCollider2D col = _attackHitbox.GetComponent<CircleCollider2D>();
        if (col == null) return;

        Gizmos.color = Color.red;
        Vector2 pos = (Vector2)_attackHitbox.transform.position + col.offset;
        float radius = col.radius * _attackHitbox.transform.lossyScale.x;
        Gizmos.DrawWireSphere(pos, radius);
    }

}
