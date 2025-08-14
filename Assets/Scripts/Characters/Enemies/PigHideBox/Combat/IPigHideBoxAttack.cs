using UnityEngine;

public interface IPigHideBoxAttack
{
    void Attack();
    void UpdateAttackTimer();
    void ResetAttack();
    void HandleAttack();
    bool IsAttacking { get; }
    float AttackCooldown { get; }
    float AttackTimer { get; }
    Collider2D HitPlayer { get; }
}
