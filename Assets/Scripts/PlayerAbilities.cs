using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    public bool chargerEnabled = true;
    public bool micEnabled = true;
    public bool remoteEnabled = true;

    public CanvasGroup chargerUI;
    public CanvasGroup remoteUI;
    public CanvasGroup micUI;

    public float destabilizationInterval = 30f;

    public GameObject currentInteractable;
    public Remote currentRemote;
    public Light2D electricLight;
    public AudioSource remoteSound;
    public AudioSource destabilizedSound;
    public Text dialogBox;

    public AudioMixer micMixer;
    public AudioMixer systemMixer;

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

    public void Destabilize()
    {
        destabilizedSound.Play();
        Invoke("Destabilize", destabilizationInterval);

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
        chargerUI.alpha = 1;
        remoteEnabled = true;
        remoteUI.alpha = 1;

        micUI.alpha = 0.1f;
        micEnabled = false;

        systemMixer.SetFloat("distortion", .3f);

        dialogBox.text = "Not enough energy. Microphone disabled. Attempting rebalance...";
    }

    void DisableCharger()
    {
        remoteEnabled = true;
        remoteUI.alpha = 1;
        micUI.alpha = 1f;
        micEnabled = true;

        chargerEnabled = false;
        chargerUI.alpha = 0.1f;
        systemMixer.SetFloat("distortion", 0f);

        dialogBox.text = "Not enough energy. Charger disabled. Attempting rebalance...";

    }

    void DisableRemote()
    {
        micUI.alpha = 1f;
        micEnabled = true;
        chargerEnabled = true;
        chargerUI.alpha = 1f;

        remoteEnabled = false;
        remoteUI.alpha = 0.1f;
        systemMixer.SetFloat("distortion", 0f);

        dialogBox.text = "Not enough energy. Remote controller disabled. Attempting rebalance...";
    }

    void Awake()
    {
        Controls = new MyDefaultControls();
        Controls.Player.Use.started += _ => Use();
        Controls.Player.Remote.performed += _ => RemoteActivate();
    }

    void FixedUpdate()
    {
        micMixer.GetFloat("volume", out float micMixerVolume);

        if (micEnabled && micMixerVolume < 0f)
        {
            micMixer.SetFloat("volume", micMixerVolume + Time.fixedDeltaTime * 20);
        } else if (!micEnabled && micMixerVolume > -80f)
        {
            float adjustAmount = micMixerVolume - Time.fixedDeltaTime * 20;
            if (adjustAmount < -80f)
            {
                adjustAmount = -80f;
            }
            micMixer.SetFloat("volume", adjustAmount);
        }

        if (expandElectricLight)
        {
            electricLight.intensity += Time.fixedDeltaTime * 10;
        }
        else if (electricLight.intensity > 0)
        {
            electricLight.intensity -= Time.fixedDeltaTime * 10;
        }
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
