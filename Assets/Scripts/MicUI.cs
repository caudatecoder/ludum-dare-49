using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (AudioReader))]
[RequireComponent(typeof (Canvas))]
public class MicUI : MonoBehaviour
{
    public Gradient gradient;
    public Slider slider;
    public Image micLevelDisplay;

    private AudioReader audioReader;
    private Canvas canvas;
    private GameObject player;
    private bool playerIsNear = false;
    private bool micEnabled = true;

    private void Start()
    {
        audioReader = GetComponent<AudioReader>();
        canvas = GetComponent<Canvas>();

        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerAbilities>().MicStatusEvent.AddListener(SetMicStatus);
    }

    private void Update()
    {
        slider.value = audioReader.output;
        micLevelDisplay.color = gradient.Evaluate(audioReader.output);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && player.GetComponent<PlayerAbilities>().micEnabled)
        {
            playerIsNear = true;
            UpdateUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            playerIsNear = false;
            UpdateUI();
        }
    }

    void SetMicStatus(bool enabled)
    {
        micEnabled = enabled;
        UpdateUI();
    }

    void UpdateUI()
    {
        canvas.enabled = micEnabled && playerIsNear;
        audioReader.enabled = micEnabled && playerIsNear;
    }
}
