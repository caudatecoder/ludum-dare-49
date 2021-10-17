using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Audio;

public class Connector : MonoBehaviour
{
    public Light2D electicLight;
    public AudioSource[] warningsLong;
    public AudioSource[] warningsShort;
    public AudioSource electricSound;
    public Animator animator;
    public float timeBeforeActivation = 3f;

    private bool wasActive = false;
    private bool isActive = false;

    private void Start()
    {
        InvokeRepeating("FlickLight", timeBeforeActivation, 0.12f);

        foreach (AudioSource wl in warningsLong)
        {
            wl.Play();
        }

        foreach (AudioSource ws in warningsShort)
        {
            ws.Play();
        }
    }

    private void FixedUpdate()
    {
        if (timeBeforeActivation > 1.2f)
        {
            warningsLong[0].volume = 1;
            warningsLong[1].volume = 1;

            if (warningsShort[0].volume > 0)
                changeWarningsSound(warningsShort, -5);
        } else if (timeBeforeActivation > 0)
        {
            warningsShort[0].volume = 1;
            warningsShort[1].volume = 1;

            if (warningsLong[0].volume > 0)
                changeWarningsSound(warningsLong, -5);
        }
        else
        {
            if (warningsShort[0].volume > 0)
                changeWarningsSound(warningsShort, -5);
            if (warningsLong[0].volume > 0)
                changeWarningsSound(warningsLong, -5);

            if (!wasActive)
            {
                wasActive = true;
                Activate();
            }
        }

        if (timeBeforeActivation > 0)
            timeBeforeActivation -= Time.fixedDeltaTime;
    }

    private void changeWarningsSound(AudioSource[] warnings, float modifier)
    {
        foreach (AudioSource warning in warnings)
        {
            warning.volume += Time.fixedDeltaTime * modifier;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive && collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("Die", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isActive && collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("Die", false);
        }
    }



    private void Activate()
    {
        isActive = true;
        electricSound.mute = false;
        animator.SetBool("Active", true);

        Invoke("ResetActivation", 1f);
    }

    private void ResetActivation()
    {
        isActive = false;
        wasActive = false;
        electricSound.mute = true;
        animator.SetBool("Active", false);

        timeBeforeActivation = Random.Range(1,10) % 3 == 0 ? Random.Range(1.8f, 2.2f) : Random.Range(0.1f, 1f);
    }

    private void FlickLight()
    {
        if (isActive)
        {
            electicLight.enabled = !electicLight.enabled;
        } else
        {
            electicLight.enabled = false;
        }
    }
}
