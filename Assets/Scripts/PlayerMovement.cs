using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public AudioSource movementSound;
    public CinemachineVirtualCamera vCam;

    private MyDefaultControls Controls;
    private float movementValue = 0f;
    private Animator animator;
    private CinemachineFramingTransposer transposer;
    private float screenYwas;

    void Awake()
    {
        Controls = new MyDefaultControls();

        Controls.Player.Movement.started += ctx => MovementStart(ctx);
        Controls.Player.Movement.canceled += ctx => MovementStop();

        Controls.Player.Look.started += ctx => LookStart(ctx);
        Controls.Player.Look.canceled += ctx => LookStop();

        animator = GetComponent<Animator>();
        transposer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
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

    void LookStart(InputAction.CallbackContext ctx)
    {
        screenYwas = transposer.m_ScreenY;

        transposer.m_ScreenY += ctx.ReadValue<float>() * 0.5f;
    }

    void LookStop()
    {
        transposer.m_ScreenY = screenYwas;
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
