using UnityEngine;

public interface IPigThrowBoxAttack
{
    void Attack();
    void UpdateAttackTimer();
    void ResetAttack();
    bool IsAttacking { get; }
    float AttackCooldown { get; }
    float AttackTimer { get; }
}
