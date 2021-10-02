using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Battery : MonoBehaviour
{
    public Door door;
    public Animator animator;
    public Light2D indicatorLight;
    private Color indicatorOpened = new Color(0, 1, 0);

    public void Interact(GameObject _)
    {
        animator.SetBool("Charged", true);
        indicatorLight.color = indicatorOpened;
        Invoke("OpenDoor", 1f);
    }

    private void OpenDoor()
    {
        door.Toggle();
    }
}
