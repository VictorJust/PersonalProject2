using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatGround : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatLength;

    void Start()
    {
        startPos = transform.position;
        repeatLength = GetComponent<BoxCollider>().size.z;
    }

    void Update()
    {
        // Make ground move (isn't smooth enough yet)
        if (transform.position.z < startPos.z - repeatLength)
        {
            transform.position = startPos;
        }
    }
}
