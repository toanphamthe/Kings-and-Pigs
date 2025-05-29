using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// Applies damage to the object.
    /// </summary>
    /// <param name="damage">The amount of damage to apply.</param>
    void TakeDamage(int damage, Vector2 attackerPosition);
    /// <summary>
    /// Checks if the object is stunned.
    /// </summary>
    bool IsStunned();
}