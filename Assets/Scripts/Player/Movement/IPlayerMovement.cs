public interface IPlayerMovement
{
    void HandleMovement();
    void Jump();

    bool IsGrounded { get; }
}
