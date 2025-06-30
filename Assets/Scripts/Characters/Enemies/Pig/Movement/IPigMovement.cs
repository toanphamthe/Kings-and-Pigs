using UnityEngine;

public interface IPigMovement
{
    bool IsGrounded { get; }
    void HandleMovement();
    void FlipDirection();
    float IdleTimer { get; }
    float IdleDuration { get; }
    void ResetIdleTimer();
    void UpdateIdleTimer();
}
