using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Audio;
using UnityEngine.Events;

public class PlayerAbilities : MonoBehaviour
{
    public bool chargerEnabled = true;
    public bool micEnabled = true;
    public bool remoteEnabled = true;

    public float destabilizationInterval = 30f;

    public GameObject currentInteractable;
    public Remote currentRemote;
    public Light2D electricLight;
    public AudioSource remoteSound;
    public AudioSource destabilizedSound;

    public AudioMixer micMixer;
    public AudioMixer systemMixer;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    [Header("Events")]
    [Space]
    public BoolEvent ChargerStatusEvent;
    public BoolEvent RemoteStatusEvent;
    public BoolEvent MicStatusEvent;

    private MyDefaultControls Controls;
    private bool expandElectricLight = false;

    void Awake()
    {
        if (ChargerStatusEvent == null)
            ChargerStatusEvent = new BoolEvent();

        if (RemoteStatusEvent == null)
            RemoteStatusEvent = new BoolEvent(); 
        
        if (MicStatusEvent == null)
            MicStatusEvent = new BoolEvent();

        Controls = new MyDefaultControls();
        Controls.Player.Use.started += _ => Use();
        Controls.Player.Remote.performed += _ => RemoteActivate();
    }

    void FixedUpdate()
    {
        UpdateMicMixer();
        UpdateElectricLight();
    }
    private void UpdateMicMixer()
    {
        micMixer.GetFloat("volume", out float micMixerVolume);

        if (micEnabled && micMixerVolume < 0f)
        {
            micMixer.SetFloat("volume", micMixerVolume + Time.fixedDeltaTime * 20);
        }
        else if (!micEnabled && micMixerVolume > -80f)
        {
            float adjustAmount = micMixerVolume - Time.fixedDeltaTime * 20;
            if (adjustAmount < -80f)
            {
                adjustAmount = -80f;
            }
            micMixer.SetFloat("volume", adjustAmount);
        }
    }

    private void UpdateElectricLight()
    {
        if (expandElectricLight)
        {
            electricLight.intensity += Time.fixedDeltaTime * 10;
        }
        else if (electricLight.intensity > 0)
        {
            electricLight.intensity -= Time.fixedDeltaTime * 10;
        }
    }

    public void SetInteractable(GameObject interactable)
    {
        currentInteractable = interactable;
    }

    public void SetRemote(Remote remote)
    {
        currentRemote = remote;
    }

    public void Destabilize()
    {
        destabilizedSound.Play();
        Invoke("Destabilize", destabilizationInterval); // TODO rework

        if (chargerEnabled && micEnabled && remoteEnabled)
        {
            DisableMic();
            return;
        }

        bool isEven = Random.Range(1, 10) % 2 == 0;

        if (!micEnabled)
        {
            if (isEven) { DisableCharger(); } else { DisableRemote(); }
            return;
        }

        if (!chargerEnabled)
        {
            if (isEven) { DisableMic(); } else { DisableRemote(); }
            return;
        }

        if (!remoteEnabled)
        {
            if (isEven) { DisableMic(); } else { DisableCharger(); }
            return;
        }

    }

    void DisableMic()
    {
        chargerEnabled = true;
        ChargerStatusEvent.Invoke(true);
        remoteEnabled = true;
        RemoteStatusEvent.Invoke(true);

        micEnabled = false;
        MicStatusEvent.Invoke(false);
        systemMixer.SetFloat("distortion", .3f);

    }

    void DisableCharger()
    {
        remoteEnabled = true;
        RemoteStatusEvent.Invoke(true);
        micEnabled = true;
        MicStatusEvent.Invoke(true);

        chargerEnabled = false;
        ChargerStatusEvent.Invoke(false);

        systemMixer.SetFloat("distortion", 0f);
    }

    void DisableRemote()
    {
        micEnabled = true;
        MicStatusEvent.Invoke(true);
        chargerEnabled = true;
        ChargerStatusEvent.Invoke(true);

        remoteEnabled = false;
        RemoteStatusEvent.Invoke(false);
        systemMixer.SetFloat("distortion", 0f);
    }

    void Use()
    {
        if (currentInteractable == null)
            return;

        if (currentInteractable.GetComponent<Battery>())
        {
            if (chargerEnabled)
            {
                currentInteractable.SendMessage("Interact", gameObject);
                expandElectricLight = true;
                Invoke("StopExpandingLight", 0.5f);
            }
        } else
        {
            currentInteractable.SendMessage("Interact", gameObject);
        }
    }

    void RemoteActivate()
    {
        if (remoteEnabled && currentRemote != null)
        {
            currentRemote.Activate();
            remoteSound.Play();
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
