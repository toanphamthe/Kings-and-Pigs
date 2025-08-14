using UnityEngine;

public interface IPigAttack
{
    void Attack();
    void UpdateAttackTimer();
    void ResetAttack();
    bool IsAttacking { get; }
    float AttackCooldown { get; }
    float AttackTimer { get; }
}
