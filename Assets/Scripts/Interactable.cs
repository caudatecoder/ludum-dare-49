using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerAbilities>().SetInteractable(gameObject);

            gameObject.SendMessage("SetPlayerNear", true, SendMessageOptions.DontRequireReceiver);
            gameObject.SendMessage("UpdateUI", null, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerAbilities>().SetInteractable(null);

            gameObject.SendMessage("SetPlayerNear", false, SendMessageOptions.DontRequireReceiver);
            gameObject.SendMessage("UpdateUI", null, SendMessageOptions.DontRequireReceiver);
        }
    }
}
