using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysPointUp : MonoBehaviour
{
    void Start()
    {
        if (transform.up.y < 0)
        {
            transform.Rotate(new Vector3(0, 0, 180));
        }
    }
}
