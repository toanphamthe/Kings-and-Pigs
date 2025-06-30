public interface IPlayerMovement
{
    bool IsGrounded { get; }
    void HandleMovement();
    float JumpStartTime { get; }
    float MinJumpDuration { get; }
    void HandleJump();
}
