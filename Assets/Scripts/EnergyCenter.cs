using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnergyCenter : MonoBehaviour
{
    public Light2D indicatorLight;
    public AudioSource unstableSound;
    public AudioSource stableSound;
    public Animator sceneTransitionAnimator;

    private bool isStable = false;

    public void Interact(GameObject player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        isStable = true;
        stableSound.Play();
        StartCoroutine(Final());
    }

    private void Start()
    {
        InvokeRepeating("BlinkIndicator", 0f, 0.25f);
        InvokeRepeating("MakeSound", 0f, 2f);
    }

    void BlinkIndicator()
    {
        if(isStable)
        {
            indicatorLight.color = new Color(0, 1, 0);
            indicatorLight.enabled = true;
        } else
        {
            indicatorLight.enabled = !indicatorLight.enabled;
        }
    }
    void MakeSound()
    {
        if (!isStable)
        {
            unstableSound.Play();
        }
    }

    IEnumerator Final()
    {
        yield return new WaitForSeconds(1);

        sceneTransitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
