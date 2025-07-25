using UnityEngine;

public interface IPigWithMatchAttack
{
    float AttackTimer { get; }
    float AttackCooldown { get; }
    void UpdateAttackTimer();
    void ResetAttackTimer();
    void PerformAttack();
}
