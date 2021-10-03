using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Connector : MonoBehaviour
{
    public Light2D electicLight;
    public AudioSource warningLong;
    public AudioSource warningShort;
    public AudioSource electricSound;
    public Animator animator;
    public float timeBeforeActivation = 3f;

    private bool wasActive = false;
    private bool isActive = false;

    private void Start()
    {
        InvokeRepeating("FlickLight", timeBeforeActivation, 0.12f);
        warningLong.Play();
        warningShort.Play();
    }

    private void FixedUpdate()
    {
        if (timeBeforeActivation > 1.2f)
        {
            warningLong.volume = 1;

            if (warningShort.volume > 0)
                warningShort.volume -= Time.fixedDeltaTime * 5;
        } else if (timeBeforeActivation > 0)
        {
            warningShort.volume = 1;

            if (warningLong.volume > 0)
                warningLong.volume -= Time.fixedDeltaTime * 5;
        } else
        {
            if (warningShort.volume > 0)
                warningShort.volume -= Time.fixedDeltaTime * 5;
            if (warningLong.volume > 0)
                warningLong.volume -= Time.fixedDeltaTime * 5;

            if (!wasActive)
            {
                wasActive = true;
                Activate();
            }
        }

        if (timeBeforeActivation > 0)
            timeBeforeActivation -= Time.fixedDeltaTime;
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
