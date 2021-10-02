using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Remote : MonoBehaviour
{
    public GameObject target;
    public Light2D indicatorLight;

    private int blinkCount = 0;

    public void Activate()
    {
        BlinkIndicatorOn();
        target.SendMessage("Toggle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerAbilities>().SetRemote(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerAbilities>().SetRemote(null);
        }
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
