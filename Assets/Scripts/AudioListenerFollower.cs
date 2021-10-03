using UnityEngine;

public class AudioListenerFollower : MonoBehaviour
{
    public GameObject target;
    
    void FixedUpdate()
    {
        transform.position = target.transform.position;
    }
}
