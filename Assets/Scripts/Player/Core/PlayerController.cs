using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private IPlayerMovement movement;
    private IPlayerAnimation playerAnimation;
    private IPlayerInput input;

    private void Awake()
    {
        if (movement == null)
        {
            movement = GetComponent<IPlayerMovement>();
        }
        if (input == null)
        {
            input = GetComponent<IPlayerInput>();
        }
        if (playerAnimation == null)
        {
            playerAnimation = GetComponent<IPlayerAnimation>();
        }
    }

    private void Update()
    {
        input.HandleInput();
        movement.HandleMovement();
    }

    private void LateUpdate()
    {
        playerAnimation.HandleAnimation(movement);
    }
}
