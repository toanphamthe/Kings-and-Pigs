using UnityEngine;

public interface IPlayerDamageable
{
    void TakeDamage(int damage, Vector2 attackerPosition);
    bool IsStunned();
}
