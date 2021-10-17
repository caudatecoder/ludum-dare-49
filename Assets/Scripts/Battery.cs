using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Battery : MonoBehaviour
{
    public Door door;
    public Animator animator;
    public Light2D indicatorLight;
    public AudioSource chargeSound;
    public Canvas uiCanvas;

    private Color indicatorOpened = new Color(0, 1, 0);
    private bool isCharged = false;
    private GameObject player;
    private bool playerIsNear = false;
    private bool chargerEnabled = true;

    private void Start()
    {

        player = GameObject.FindWithTag("Player");
        chargerEnabled = player.GetComponent<PlayerAbilities>().chargerEnabled;
        player.GetComponent<PlayerAbilities>().ChargerStatusEvent.AddListener(SetChargerStatus);
    }


    public void Interact(GameObject _)
    {
        if (isCharged)
            return;

        isCharged = true;
        UpdateUI();

        animator.SetBool("Charged", true);
        indicatorLight.color = indicatorOpened;
        chargeSound.Play();

        Invoke("UnlockDoor", 1f);
    }

    public void UpdateUI()
    {
        uiCanvas.enabled = chargerEnabled && playerIsNear && !isCharged;
    }

    public void SetPlayerNear(bool isNear)
    {
        playerIsNear = isNear;
    }

    private void SetChargerStatus(bool enabled)
    {
        chargerEnabled = enabled;
        UpdateUI();
    }


    private void UnlockDoor()
    {
        door.canBeOpened = true;
        door.Toggle();
    }
}
