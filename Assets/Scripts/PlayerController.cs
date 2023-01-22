using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float forwardInput;
    public float sidewardInput;
    public int lives;
    private float zBound = 4.5f;

    private Rigidbody playerRb;
    private GameObject focalPoint;

    private GameManager gameManager;

    public bool gameOver = false;


    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Make the player move
        forwardInput = Input.GetAxis("Vertical");
        sidewardInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        playerRb.AddForce(focalPoint.transform.right * sidewardInput * speed);

        // Not to let the player go above/below the screen
        if (playerRb.position.z > zBound || playerRb.position.z < -zBound)
        { 
            playerRb.velocity = Vector3.zero;
        }

        if (gameManager.lives < 1)
        {
            gameOver = true;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.AddLives(-1);
        }
    }
}
