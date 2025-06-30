public interface IPlayerAttackable
{
    void Attack();
    void StopAttack();
    bool IsAttacking { get; }
}
