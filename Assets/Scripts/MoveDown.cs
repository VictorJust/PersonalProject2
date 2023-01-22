using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 1f;
    void Update()
    {
        // Make objects move "down" the screen
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);            
    }
}
