using UnityEngine;

public class Staircase : MonoBehaviour
{
    public GameObject linkedStaircase;

    private GameObject player;

    public void Interact(GameObject playerInteracted)
    {
        player = playerInteracted;

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.transform.position = linkedStaircase.transform.position;

        Invoke("ReturnPlayer", 0.5f);
    }

    public void ReturnPlayer()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
    }
}
