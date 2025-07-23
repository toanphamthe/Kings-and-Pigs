using UnityEngine;

public class PlayerAttack : MonoBehaviour, IPlayerAttackable
{
    [SerializeField] private GameObject _attackHitbox;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _boxLayer;
    [SerializeField] private int _damageAmount;
    [SerializeField] private bool _isAttacking;

    public bool IsAttacking => _isAttacking;

    private void Awake()
    {
        _attackHitbox = transform.Find("AttackHitBox").gameObject;
        _attackHitbox.SetActive(false);
    }

    // This method is called to enable the attack hitbox and start the attack.
    public void Attack()
    {
        _isAttacking = true;
        float attackSize = _attackHitbox.gameObject.GetComponent<CircleCollider2D>().radius;

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_attackHitbox.transform.position, attackSize, _enemyLayer);
        Collider2D boxHit = Physics2D.OverlapCircle(_attackHitbox.transform.position, attackSize, _boxLayer);

        foreach (Collider2D enemy in enemiesHit)
        {
            enemy.GetComponent<IEnemyHit>().TakeDamage(_damageAmount, gameObject.transform.position);
        }

        if (boxHit != null)
        {
            boxHit?.GetComponent<ItemBox>()?.TakeDamage();
        }

    }

    // This method is called to disable the attack hitbox and stop the attack.
    public void StopAttack()
    {
        _isAttacking = false;
    }

    // This method is called to enable the attack hitbox.
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
