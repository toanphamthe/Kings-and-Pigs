using System.Security.Cryptography;
using UnityEngine;

public interface IEnemyAttack
{
    void Attack();
    void UpdateAttackTimer();
    void ResetAttack();
    bool IsAttacking { get; }
    float AttackCooldown { get; }
    float AttackTimer { get; }
}
