using System.Collections;
using UnityEngine;

public class PigHideBoxHit : EnemyHit
{
    [SerializeField] private GameObject _breakEffectPrefab;
    [SerializeField] private GameObject _pig;
    [SerializeField] private bool _hasBeenHit;

    public override void TakeDamage(int damage, Vector2 attackerPosition)
    {
        base.TakeDamage(damage, attackerPosition);
        HitHandler();
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
        return base.ApplyKnockback(attackerPosition);
    }

    private void HitHandler()
    {
        if (_hasBeenHit) return;
        _hasBeenHit = true;

        if (_breakEffectPrefab != null)
            Instantiate(_breakEffectPrefab, transform.position, Quaternion.identity);
        if (_pig != null)
        {
            GameObject pig = Instantiate(_pig, transform.position, Quaternion.identity);
            Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null) return;
            pig.gameObject.GetComponent<PigHit>()?.TakeDamage(0, player.position);
        }
        Destroy(gameObject);
    }
}
