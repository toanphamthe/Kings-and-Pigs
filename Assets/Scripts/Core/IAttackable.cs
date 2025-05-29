public interface IAttackable
{
    /// <summary>
    /// Initiates an attack on the target.
    /// </summary>
    void Attack();
    /// <summary>
    /// 
    /// </summary>
    bool IsAttacking { get; }
}