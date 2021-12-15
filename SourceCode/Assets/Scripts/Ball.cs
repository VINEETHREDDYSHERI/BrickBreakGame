using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isBallOnPlay = false; // If the ball is in play or not
    private float ballCount = 1;
    private float ballSpeed = 400.0f;

    private Rigidbody rigidbody;
    private AudioSource audioSource;
    private GameManager gameManager;
    private GameObject paddle;

    public GameObject brickExplosion;
    public GameObject lifePowerUp;
    public GameObject paddlePowerUp;
    public GameObject ballPowerUp;

    public AudioClip bounceSound;
    public AudioClip brickDestroySound;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // GameOver
        if(gameManager.isGameOver)
        {
            return;
        }

        ballCount = GameObject.FindGameObjectsWithTag("Ball").Length;
        paddle = GameObject.Find("Player").transform.GetChild(0).gameObject;
        // If the Ball is not in Play, placing the ball at the top of Paddle 
        if (!isBallOnPlay)
        {
            transform.position = paddle.transform.position + Vector3.up * 0.35f;
        }
        // On the press of SPACE, the ball movement will start. And can press only once that is when the ball is not in play
        if(Input.GetKeyDown(KeyCode.Space) && !isBallOnPlay)
        {
            isBallOnPlay = true;
            rigidbody.AddForce(Vector3.up * ballSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // To check if the Ball fallen down the screen
        if(other.CompareTag("BottomWall"))
        {
            // If the ball count in the game greater than 1. Then destrying the ball.
            // Else decreasing the lives count by 1 and reseting the ball position at the top of paddle.
            if(ballCount>1)
            {
                Destroy(gameObject);
            }
            else
            {
                rigidbody.velocity = Vector3.zero;
                isBallOnPlay = false;
                gameManager.UpdateLives(-1);
            }   
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // To check if the ball has collided with the brick or not
        if(collision.gameObject.CompareTag("Brick"))
        {
            // Getting the random number from 0.t to 1.0.
            float probPowerUp = Random.Range(0.0f,1.0f);
            // Based upon the random Value obtain creating the powerups.
            if(probPowerUp<=0.05)
            {
                // LifePowerUp
                Instantiate(lifePowerUp, collision.transform.position, lifePowerUp.transform.rotation);
            }
            else if(probPowerUp > 0.05 && probPowerUp <=  0.15 && ballCount<=3)
            {
                // PaddleSize PowerUp
                Instantiate(paddlePowerUp, collision.transform.position, paddlePowerUp.transform.rotation);
            }
            else if(probPowerUp > 0.15 && probPowerUp <= 0.2)
            {
                // BallCount PowerUp
                Instantiate(ballPowerUp, collision.transform.position, ballPowerUp.transform.rotation);
            }
            // Destrying the brick, updating the score.
            Destroy(collision.gameObject);
            gameManager.UpdateScore(1);
            // Creating the Explosion effect and also the SoundEffect
            GameObject newBrickExplosion = Instantiate(brickExplosion, collision.transform.position, brickExplosion.transform.rotation);
            Destroy(newBrickExplosion, 3.0f);
            audioSource.PlayOneShot(brickDestroySound,0.1f);
        }
        else if(isBallOnPlay)
        {
            // If the ball bounces off anything other than the brick. The below soundeffect will be played.
            audioSource.PlayOneShot(bounceSound,0.1f);
        }
    }
}
