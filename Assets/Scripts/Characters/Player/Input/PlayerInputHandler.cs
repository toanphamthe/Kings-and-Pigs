using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputHandler : MonoBehaviour, IPlayerInput
{
    [SerializeField] private bool _disableInput;

    public bool IsAttacking { get; private set; }
    public bool IsJumping { get; private set; }
    public float Horizontal { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (_disableInput) return;

        Horizontal = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (_disableInput) return;

        if (context.performed)
            IsJumping = true;
        else if (context.canceled)
            IsJumping = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (_disableInput) return;

        if (context.performed)
            IsAttacking = true;
        else if (context.canceled)
            IsAttacking = false;
    }

    public void DisableInput() => _disableInput = true;
    public void EnableInput() => _disableInput = false;
}
