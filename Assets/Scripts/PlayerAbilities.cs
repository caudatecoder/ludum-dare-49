using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{
    public bool chargerEnabled = true;
    public bool micEnabled = true;
    public bool CPREnabled = true;
    public GameObject currentInteractable;
    public Remote currentRemote;

    private MyDefaultControls Controls;

    public void SetInteractable(GameObject interactable)
    {
        currentInteractable = interactable;
    }

    public void SetRemote(Remote remote)
    {
        currentRemote = remote;
    }
    void Awake()
    {
        Controls = new MyDefaultControls();
        Controls.Player.Use.started += _ => Use();
        Controls.Player.Remote.performed += _ => RemoteActivate();
    }

    void Use()
    {
        if (currentInteractable != null)
            currentInteractable.SendMessage("Interact", gameObject);
    }

    void RemoteActivate()
    {
        if (currentRemote != null)
            currentRemote.Activate();
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
