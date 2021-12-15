using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // GameManager script
    private GameManager gameManager;
    private GameObject player;

    public GameObject ballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // PowerUps
        if (other.CompareTag("LifePowerUp"))
        {
            // Life PowerUp
            Destroy(other.gameObject);
            gameManager.UpdateLives(1);
        }
        else if(other.CompareTag("PaddlePowerUp"))
        {
            // Paddle size PowerUp
            Destroy(other.gameObject);
            player.transform.localScale = Vector3.one * 1.5f;
        }
        else if(other.CompareTag("BallPowerUp"))
        {
            // Ball PowerUp
            Destroy(other.gameObject);
            Instantiate(ballPrefab, transform.position + Vector3.up * 0.35f, ballPrefab.transform.rotation);
        }
    }
}
