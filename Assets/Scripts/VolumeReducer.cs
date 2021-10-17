using UnityEngine;
using UnityEngine.Audio;

public class VolumeReducer : MonoBehaviour
{
    public AudioMixer mixer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mixer.SetFloat("volume", -40f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mixer.SetFloat("volume", 0f);
        }
    }

}
