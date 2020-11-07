using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float destructDistance = 20; // how far off screen may obj travel before being destroyed
    private bool freeze = false;

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
        if (!freeze)
            SetSpeed(GameManager.Instance.GetCurrentSpeed());
        if (transform.tag != "Ground" && -transform.position.x >= destructDistance)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        // re-initialize speed to GameManager
        // this function is called by GameManager when sprinting or otherwise changing the 
        Vector2 move = rb2d.velocity;
        move.x = -speed;
        rb2d.velocity = move;
    }

    public void SetFreeze(bool freeze)
    {
        this.freeze = freeze;
        if (freeze)
            SetSpeed(0f);
        else
            SetSpeed(GameManager.Instance.GetCurrentSpeed());
    }
}
