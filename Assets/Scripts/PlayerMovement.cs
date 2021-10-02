using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    private MyDefaultControls Controls;
    private float movementValue = 0f;

    void Awake()
    {
        Controls = new MyDefaultControls();
        Controls.Player.Movement.started += ctx => MovementStart(ctx);
        Controls.Player.Movement.canceled += ctx => MovementStop();
    }
    private void FixedUpdate()
    {
        controller.Move(movementValue);
    }

    void MovementStart(InputAction.CallbackContext ctx)
    {
        movementValue = ctx.ReadValue<float>();
    }

    void MovementStop()
    {
        movementValue = 0f;
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }
}
