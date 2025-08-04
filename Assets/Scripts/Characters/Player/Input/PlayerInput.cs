using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private bool _disableInput;
    public bool IsAttacking { get; private set; }
    public bool IsJumping { get; private set; }
    public float Horizontal { get; private set; }

    private void Update()
    {
        if (!_disableInput)
        {
            HandleInput();
        }
    }

    // This method handles player input for attacking and jumping actions.
    public void HandleInput()
    {
        IsAttacking = Input.GetKeyDown(KeyCode.C);

        IsJumping = Input.GetKeyDown(KeyCode.X);

        Horizontal = Input.GetAxisRaw("Horizontal");
    }

    public void DisableInput()
    {
        _disableInput = true;
    }

    public void EnableInput()
    {
        _disableInput = false;
    }
}
