using UnityEngine;

public class Remote : MonoBehaviour
{
    public GameObject target;

    public void Activate()
    {
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
}
