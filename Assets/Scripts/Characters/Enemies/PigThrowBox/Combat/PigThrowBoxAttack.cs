using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PigThrowBoxAttack : MonoBehaviour, IPigThrowBoxAttack
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private float _attackRange;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private LayerMask _playerLayers;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private float _attackTimer;
    private ObjectPool _objPool;

    [SerializeField] private float projectileHeight;
    [SerializeField] private float gravityScale;

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

    //
    public void ThrowBoxAtPlayer()
    {
        Player player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        if (player == null) return;

        FlipToFacePlayer(player.transform);

        Vector2 startPos = _throwPoint.position;
        Vector2 targetPos = player.transform.position;

        float gravity = Physics2D.gravity.y * gravityScale;

        Vector2 velocity = CalculateThrowVelocity(startPos, targetPos, projectileHeight, gravity);

        _objPool = GetComponent<ObjectPool>();
        PooledObject thrownObj = _objPool.GetPooledObject();
        if (thrownObj == null) return;

        thrownObj.transform.position = startPos;
        thrownObj.transform.rotation = Quaternion.identity;
        thrownObj.gameObject.SetActive(false);

        ThrownObject throwable = thrownObj.GetComponent<ThrownObject>();
        throwable?.Initialize(_objPool);

        thrownObj.gameObject.SetActive(true);

        Rigidbody2D rb = thrownObj.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = gravityScale;
            rb.linearVelocity = velocity;
        }
    }

    private void FlipToFacePlayer(Transform playerTransform)
    {
        if (playerTransform == null)
            return;

        Vector3 scale = transform.localScale;

        if (playerTransform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1f;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
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

    public static Vector2 CalculateThrowVelocity(Vector2 startPos, Vector2 targetPos, float height, float gravity)
    {
        float displacementY = targetPos.y - startPos.y;
        Vector2 displacementX = new Vector2(targetPos.x - startPos.x, 0);

        float timeToApex = Mathf.Sqrt(-2 * height / gravity);
        float totalTime = timeToApex + Mathf.Sqrt(2 * (displacementY - height) / gravity);

        Vector2 velocityY = Vector2.up * Mathf.Sqrt(-2 * gravity * height);
        Vector2 velocityX = displacementX / totalTime;

        return velocityX + velocityY;
    }
}
