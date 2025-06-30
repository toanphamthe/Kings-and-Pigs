using UnityEngine;

public class PigAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _playerLayers;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private float _attackTimer;
    public bool IsAttacking => _isAttacking;
    public float AttackCooldown => _attackCooldown;
    public float AttackTimer => _attackTimer;

    public Collider2D HitPlayer { get; private set; }

    private void Update()
    {
        PlayerCheck();
    }

    // This method is called by the enemy state machine when the enemy is in an attacking state.
    private void PlayerCheck()
    {
        HitPlayer = Physics2D.OverlapCircle(_attackPoint.position, _attackRange, _playerLayers);
        if (HitPlayer == null)
        {
            return;
        }
    }

    // This method is called by the enemy state machine when the enemy is in an attacking state.
    public void Attack()
    {
        _attackTimer = 0f;
        _isAttacking = true;
    }

    public void UpdateAttackTimer()
    {
        _attackTimer += Time.deltaTime;
    }

    // This method is called by the enemy state machine when the enemy is in an attacking state.
    public void DealDamage()
    {
        Player player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        if (player != null)
        {
            player.Hit.TakeDamage(1, gameObject.transform.position);
        }
    }

    // This method is called by the enemy state machine when the enemy is in an attacking state.
    public void ResetAttack()
    {
        _isAttacking = false;
        HitPlayer = null;
    }

    // This method is called by the enemy state machine when the enemy is in an attacking state.
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
