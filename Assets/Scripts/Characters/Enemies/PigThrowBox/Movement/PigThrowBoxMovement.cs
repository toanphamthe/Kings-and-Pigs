using UnityEngine;

public class PigThrowBoxMovement : EnemyMovement
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    // This method is called by the enemy state machine to handle movement logic.
    protected override void GroundCheck()
    {
        base.GroundCheck();
    }

    protected override void WallCheck()
    {
        base.WallCheck();
    }

    // This method is called by the enemy state machine to handle movement logic.
    public override void HandleMovement()
    {
        base.HandleMovement();
    }

    // This method is called by the enemy state machine to flip the direction of the pig.
    public override void FlipDirection()
    {
        base.FlipDirection();
    }

    // This method is called by the enemy state machine to update the idle timer.
    public override void UpdateIdleTimer()
    {
        base.UpdateIdleTimer();
    }

    // This method is called by the enemy state machine to reset the idle timer.
    public override void ResetIdleTimer()
    {
        base.ResetIdleTimer();
    }

    // This method is called by the enemy state machine to draw gizmos in the editor for debugging purposes.
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
