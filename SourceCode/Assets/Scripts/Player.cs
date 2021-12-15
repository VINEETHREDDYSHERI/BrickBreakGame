using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // GameManager script
    private GameManager gameManager;

    public float speed = 10.0f;
    public float xRange = 9.3f;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // GameOver
        if(gameManager.isGameOver)
        {
            return;
        }

        // Player style of play i.e either Mouse or keyboard
        if(gameManager.mouseControl)
        {
            float currentX = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 50)).x / 4;
            transform.position = new Vector3(currentX, transform.position.y, 0);
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
        }

        // Player restriction within the screen
        if (transform.position.x <= -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }
}

