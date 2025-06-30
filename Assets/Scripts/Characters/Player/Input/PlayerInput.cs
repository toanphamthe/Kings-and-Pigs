using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public bool IsAttacking { get; private set; }
    public bool IsJumping { get; private set; }
    public float Horizontal => Input.GetAxisRaw("Horizontal"); // This property returns the horizontal input axis value, which can be used for movement or other actions.

    private void Update()
    {
        HandleInput();
    }

    // This method handles player input for attacking and jumping actions.
    public void HandleInput()
    {
        IsAttacking = Input.GetKeyDown(KeyCode.C);

        IsJumping = Input.GetKeyDown(KeyCode.X);
    }
}
