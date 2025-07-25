using UnityEngine;

public class PigWithMatchAttack : MonoBehaviour, IPigWithMatchAttack
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _attackTimer;

    [SerializeField] private GameObject _cannonBallPrefab;
    [SerializeField] private Transform _cannonBallSpawnPoint;
    [SerializeField] private float _cannonBallSpeed;

    public float AttackTimer => _attackTimer;
    public float AttackCooldown => _attackCooldown;

    public void UpdateAttackTimer()
    {
        _attackTimer += Time.deltaTime;
    }

    public void ResetAttackTimer()
    {
        _attackTimer = 0f;
    }

    public void PerformAttack()
    {
        GameObject cannonBall = Instantiate(_cannonBallPrefab, _cannonBallSpawnPoint.position, Quaternion.identity);
    }
}
