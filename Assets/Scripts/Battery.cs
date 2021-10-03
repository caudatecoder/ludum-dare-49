using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Battery : MonoBehaviour
{
    public Door door;
    public Animator animator;
    public Light2D indicatorLight;
    public AudioSource chargeSound;

    private Color indicatorOpened = new Color(0, 1, 0);
    private bool isCharged = false;

    public void Interact(GameObject _)
    {
        if (isCharged)
            return;

        isCharged = true;

        animator.SetBool("Charged", true);
        indicatorLight.color = indicatorOpened;
        chargeSound.Play();

        Invoke("UnlockDoor", 1f);
    }

    private void UnlockDoor()
    {
        door.canBeOpened = true;
        door.Toggle();
    }
}
