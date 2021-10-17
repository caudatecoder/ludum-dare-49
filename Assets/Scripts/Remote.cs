using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Remote : MonoBehaviour
{
    public GameObject target;
    public Light2D indicatorLight;
    public Canvas uiCanvas;

    private int blinkCount = 0;
    private GameObject player;
    private bool playerIsNear = false;
    private bool remoteEnabled = true;

    private void Start()
    {

        player = GameObject.FindWithTag("Player");
        remoteEnabled = player.GetComponent<PlayerAbilities>().remoteEnabled;
        player.GetComponent<PlayerAbilities>().RemoteStatusEvent.AddListener(SetRemoteStatus);
    }

    public void Activate()
    {
        BlinkIndicatorOn();
        target.SendMessage("Toggle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerAbilities>().SetRemote(this);

            playerIsNear = true;
            UpdateUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerAbilities>().SetRemote(null);

            playerIsNear = false;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        uiCanvas.enabled = remoteEnabled && playerIsNear;
    }

    private void SetRemoteStatus(bool enabled)
    {
        remoteEnabled = enabled;
        UpdateUI();
    }

    private void BlinkIndicatorOn()
    {
        indicatorLight.enabled = true;

        Invoke("BlinkIndicatorOff", 0.25f);
    }

    private void BlinkIndicatorOff()
    {
        indicatorLight.enabled = false;

        if (blinkCount < 2)
        {
            blinkCount++;
            Invoke("BlinkIndicatorOn", 0.25f);
        } else
        {
            blinkCount = 0;
        }
    }
}
