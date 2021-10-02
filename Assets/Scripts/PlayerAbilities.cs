using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerAbilities : MonoBehaviour
{
    public bool chargerEnabled = true;
    public bool micEnabled = true;
    public bool CPREnabled = true;
    public GameObject currentInteractable;
    public Remote currentRemote;
    public Light2D electricLight;
    public AudioSource remoteSound;

    private MyDefaultControls Controls;
    private bool expandElectricLight = false;

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
        if (currentInteractable == null)
            return;

        currentInteractable.SendMessage("Interact", gameObject);

        if (currentInteractable.GetComponent<Battery>())
        {
            expandElectricLight = true;
            Invoke("StopExpandingLight", 0.5f);
        }
    }

    void RemoteActivate()
    {
        if (currentRemote != null)
        {
            currentRemote.Activate();
            remoteSound.Play();
        }
    }

    private void FixedUpdate()
    {
        if (expandElectricLight)
        {
            electricLight.intensity += Time.fixedDeltaTime * 10;
        } else if (electricLight.intensity > 0)
        {
            electricLight.intensity -= Time.fixedDeltaTime * 10;
        }
    }

    private void StopExpandingLight()
    {
        expandElectricLight = false;
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
