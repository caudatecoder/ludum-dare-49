using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public bool canBeOpened = true;
    public Animator animator;
    public BoxCollider2D colliderToDisable;
    public Light2D indicatorLight;
    public AudioSource doorSound;

    private Color indicatorClosed = new Color(1, 0, 0);
    private Color indicatorOpened = new Color(0, 1, 0);

    private void Start()
    {
        Perform();
    }

    public void Toggle()
    {
        if (!canBeOpened)
            return;

        doorSound.Play();
        isOpen = !isOpen;
        Perform();
    }

    public void Perform()
    {
        animator.SetBool("Open", isOpen);
        Invoke("toggleCollider", 0.7f);
        indicatorLight.color = isOpen ? indicatorOpened : indicatorClosed;
    }

    private void toggleCollider()
    {
        colliderToDisable.enabled = !isOpen;
    }
}
 