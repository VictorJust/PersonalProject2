using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodGuy : MonoBehaviour
{
    public GameObject goodGuy;
    public bool goodGuyIsSaved;

    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        goodGuyIsSaved = false;
    }

    private void Update()
    {
        if (goodGuyIsSaved)
        { 
            Destroy(goodGuy);
            gameManager.AddScore(1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            goodGuyIsSaved = true;
        }
    }
}
