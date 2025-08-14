using UnityEngine;

public interface IEnemyHit
{
    void TakeDamage(int damage, Vector2 attackerPosition);
    bool IsStunned();
    int GetHealth();
}
