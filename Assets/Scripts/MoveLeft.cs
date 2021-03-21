using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15f;
    private PlayerController playerController;
    private float obstacleOffset = 10f;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isGameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x + obstacleOffset < playerController.transform.position.x && transform.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        
    }
}
