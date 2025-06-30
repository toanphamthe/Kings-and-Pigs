using UnityEngine;

public interface IEnemyDamageable
{
    void TakeDamage(int damage, Vector2 attackerPosition);
    bool IsStunned();
    int GetHealth();
}
