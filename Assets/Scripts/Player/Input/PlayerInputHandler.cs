using UnityEngine;

public class PlayerInputHandler : MonoBehaviour, IPlayerInput
{
    private IPlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<IPlayerMovement>();
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.Jump();
        }
    }
}
