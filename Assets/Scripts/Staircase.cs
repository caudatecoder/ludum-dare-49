using UnityEngine;

public class Staircase : MonoBehaviour
{
    public GameObject linkedStaircase;

    public void Interact(GameObject player)
    {
        player.transform.position = linkedStaircase.transform.position;
    }
}
