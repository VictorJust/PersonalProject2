using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float zBound = -15;
    void Update()
    {
        // Destroy objects that come below the screen
        if (transform.position.z < zBound)
        { 
            Destroy(gameObject);
        }
    }
}
