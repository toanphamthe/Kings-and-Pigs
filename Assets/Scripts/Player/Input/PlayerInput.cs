using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public bool IsAttacking { get; private set; }
    public bool IsJumping { get; private set; }
    public float Horizontal => Input.GetAxisRaw("Horizontal");

    private void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        IsAttacking = Input.GetKeyDown(KeyCode.C);

        IsJumping = Input.GetKeyDown(KeyCode.X);
    }
}
