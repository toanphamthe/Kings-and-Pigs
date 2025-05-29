using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private IAttackable _playerAttack;

    private void Update()
    {
        Debug.Log("IsAttacking: " + _playerAttack.IsAttacking);
    }

    private void Awake()
    {
        _playerAttack = GetComponentInParent<IAttackable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && _playerAttack.IsAttacking)
        {
            Debug.Log("Hit enemy");
        }
    }
}
