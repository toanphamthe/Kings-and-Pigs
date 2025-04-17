public interface IPlayerInput
{
    bool IsAttacking { get; }
    bool IsJumping { get; }
    float Horizontal { get; }
}
