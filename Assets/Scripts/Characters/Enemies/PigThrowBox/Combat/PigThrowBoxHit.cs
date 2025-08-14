using System.Collections;
using UnityEngine;

public class PigThrowBoxHit : PigHit
{
    public override void TakeDamage(int damage, Vector2 attackerPosition)
    {
        base.TakeDamage(damage, attackerPosition);
    }

    public override int GetHealth()
    {
        return base.GetHealth();
    }

    public override bool IsStunned()
    {
        return base.IsStunned();
    }

    protected override IEnumerator ApplyKnockback(Vector2 attackerPosition)
    {
        yield return base.ApplyKnockback(attackerPosition);
    }
}
