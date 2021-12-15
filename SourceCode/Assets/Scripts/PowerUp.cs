using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // PowerUp will fall down the screen.
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        // Destroy the powerUp, if the player doesn't takes it and falls down the screen
        if(transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
