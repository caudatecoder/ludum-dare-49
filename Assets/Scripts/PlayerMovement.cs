using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public AudioSource movementSound;

    private MyDefaultControls Controls;
    private float movementValue = 0f;
    private Animator animator;

    void Awake()
    {
        Controls = new MyDefaultControls();
        Controls.Player.Movement.started += ctx => MovementStart(ctx);
        Controls.Player.Movement.canceled += ctx => MovementStop();

        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        controller.Move(movementValue);
    }

    void MovementStart(InputAction.CallbackContext ctx)
    {
        movementValue = ctx.ReadValue<float>();

        animator.SetFloat("Speed", Mathf.Abs(movementValue));

        if (movementSound != null)
            movementSound.Play();
    }

    void MovementStop()
    {
        movementValue = 0f;
        animator.SetFloat("Speed", 0);

        if (movementSound != null)
            movementSound.Pause();
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
