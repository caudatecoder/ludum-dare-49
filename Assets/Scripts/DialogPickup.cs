using UnityEngine;
using UnityEngine.UI;

public class DialogPickup : MonoBehaviour
{
    public string text;
    public Text textUI;
    public AudioSource notificationSound;

    private bool isDone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDone && collision.gameObject.tag == "Player")
        {
            isDone = true;
            textUI.text = text;
            notificationSound.Play();
        }
    }
}
