using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PigThrowBoxAttack : MonoBehaviour, IPigThrowBoxAttack
{
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private float _attackTimer;
    private ObjectPool _objPool;

    [SerializeField] private Vector2 _throwDirection;
    [SerializeField] private float _throwForce;
    public bool IsAttacking => _isAttacking;
    public float AttackCooldown => _attackCooldown;
    public float AttackTimer => _attackTimer;

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

    //
    public void ThrowBox()
    {
        _objPool = GetComponent<ObjectPool>();
        PooledObject thrownObj = _objPool.GetPooledObject();
        if (thrownObj == null) return;

        thrownObj.transform.position = _throwPoint.position;
        thrownObj.transform.rotation = Quaternion.identity;
        thrownObj.gameObject.SetActive(false);

        ThrownObject throwable = thrownObj.GetComponent<ThrownObject>();
        throwable?.Initialize(_objPool);

        thrownObj.gameObject.SetActive(true);

        Rigidbody2D rb = thrownObj.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = _throwDirection.normalized * _throwForce;
        }
    }

    // This method is called by the enemy state machine when the enemy is in an attacking state.
    public void ResetAttack()
    {
        _isAttacking = false;
    }

    // This method is called by the enemy state machine when the enemy is in an attacking state.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_throwPoint.position, _throwDirection.normalized);
    }
}
