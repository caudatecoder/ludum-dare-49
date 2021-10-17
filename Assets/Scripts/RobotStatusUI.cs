using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class RobotStatusUI : MonoBehaviour
{
    public float popUpTime = 3f;

    public Image chargerImage;
    public Image remoteImage;
    public Image micImage;

    public Color colorOn;
    public Color colorOff;

    private bool chargerEnabled;
    private bool remoteEnabled;
    private bool micEnabled;

    private Animator animator;
    private MyDefaultControls Controls;
    private bool show = false;
    private GameObject player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Controls = new MyDefaultControls();
        Controls.Player.DisplayStatusUI.started += _ => UpdateStatusUI();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        PlayerAbilities playerAbilities = player.GetComponent<PlayerAbilities>();

        chargerEnabled = playerAbilities.chargerEnabled;
        remoteEnabled = playerAbilities.remoteEnabled;
        micEnabled = playerAbilities.micEnabled;
        playerAbilities.ChargerStatusEvent.AddListener(SetChargerStatus);
        playerAbilities.RemoteStatusEvent.AddListener(SetRemoteStatus);
        playerAbilities.MicStatusEvent.AddListener(SetMicStatus);
    }

    private void SetChargerStatus(bool enabled)
    {
        chargerEnabled = enabled;
        UpdateIconsColors();
        PopUpStatusUI();
    }
    private void SetRemoteStatus(bool enabled)
    {
        remoteEnabled = enabled;
        UpdateIconsColors();
        PopUpStatusUI();
    }
    private void SetMicStatus(bool enabled)
    {
        micEnabled = enabled;
        UpdateIconsColors();
        PopUpStatusUI();
    }

    public void PopUpStatusUI()
    {
        if (show)
            return;

        StartCoroutine(popUpStatusUI());
    }

    IEnumerator popUpStatusUI()
    {
        UpdateStatusUI();

        yield return new WaitForSeconds(popUpTime);

        UpdateStatusUI();
    }

    private void UpdateStatusUI()
    {
        show = !show;
        animator.SetBool("Show", show);
    }

    private void UpdateIconsColors()
    {
        chargerImage.color = chargerEnabled ? colorOn : colorOff;
        remoteImage.color = remoteEnabled ? colorOn : colorOff;
        micImage.color = micEnabled ? colorOn : colorOff;
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
