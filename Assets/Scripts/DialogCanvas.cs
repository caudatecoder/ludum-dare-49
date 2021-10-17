using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogCanvas : MonoBehaviour
{
    public Text dialogText;
    public double dialogNonInteractableTime = 1;

    private DateTime dialogStartedAt;
    private Canvas canvas;
    private bool canCloseDialog = true;
    private MyDefaultControls Controls;

    private void Awake()
    {
        Controls = new MyDefaultControls();
        Controls.Player.Use.started += _ => SkipDialog();
        Controls.Player.Remote.started += _ => SkipDialog();
    }

    public void SkipDialog()
    {
        if (!canCloseDialog)
            return;

        canvas.enabled = false;
    }

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void ShowNewDialog(string text)
    {
        dialogStartedAt = DateTime.Now;
        canvas.enabled = true;
        canCloseDialog = false;
        dialogText.text = text;
    }

    private void FixedUpdate()
    {
        if (!canCloseDialog && DateTime.Now.Subtract(dialogStartedAt).TotalSeconds > dialogNonInteractableTime)
        {
            canCloseDialog = true;
            dialogText.text += "...";
        }
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
