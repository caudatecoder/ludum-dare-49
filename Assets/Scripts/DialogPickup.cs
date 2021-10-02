using UnityEngine;
using UnityEngine.UI;

public class DialogPickup : MonoBehaviour
{
    public string text;
    public Text textUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textUI.text = text;
        }
    }
}
