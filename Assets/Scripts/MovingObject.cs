using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private Rigidbody2D rb2d;

    void Start()
    {
        // start moving at the speed specified in the GameManager
        // this includes enemies, coins, the ground, etc.
        // this works by basically moving all objects towards the player instead of moving the player
        // the benefit of this is that we don't need to move the camera and player
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetSpeed(GameManager.Instance.GetCurrentSpeed());
    }

    public void SetSpeed(float speed)
    {
        // re-initialize speed to GameManager
        // this function is called by GameManager when sprinting or otherwise changing the 
        Vector2 move = rb2d.velocity;
        move.x = -speed;
        rb2d.velocity = move;
    }
}
