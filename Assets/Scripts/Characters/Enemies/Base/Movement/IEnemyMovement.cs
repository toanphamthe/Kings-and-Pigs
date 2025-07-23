using UnityEngine;

public interface IEnemyMovement
{
    bool IsGrounded { get; }
    bool IsHittingWall { get; }
    void HandleMovement();
    void FlipDirection();
    float IdleTimer { get; }
    float IdleDuration { get; }
    void ResetIdleTimer();
    void UpdateIdleTimer();
}
