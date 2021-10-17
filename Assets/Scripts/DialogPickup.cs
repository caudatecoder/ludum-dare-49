using UnityEngine;
using UnityEngine.UI;

public class DialogPickup : MonoBehaviour
{
    public string text;
    public DialogCanvas dialogCanvas;
    public AudioSource notificationSound;

    private bool isDone = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDone && collision.gameObject.tag == "Player")
        {
            isDone = true;
            dialogCanvas.ShowNewDialog(text);
            notificationSound.Play();
        }
    }
}
