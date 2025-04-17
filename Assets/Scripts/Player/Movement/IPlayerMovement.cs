public interface IPlayerMovement
{
    bool IsGrounded { get; }
    void HandleMovement();
    void HandleJump();
}
