using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform ground;
    private bool isGrounded = true;

    private Animator animator;
    private Rigidbody2D rb2d;

    public float jumpSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //isGrounded = Physics2D.OverlapCircle(ground.position, 0.15f);
        
    }
    
    public void Jump()
    {
        if (!isGrounded)
            return;
        isGrounded = false;
        animator.SetBool("Jumping", true);
        Vector2 move = rb2d.velocity;
        move.y = jumpSpeed;
        rb2d.velocity = move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            animator.SetBool("Jumping", false);
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform  t = collision.transform;
        if (t.tag == "Enemy")
        {
            ResourceManager.Instance.SlayEnemy(t.gameObject);
        }
        else if (t.tag == "Coin")
        {
            ResourceManager.Instance.CollectCoin(t.gameObject);
        }
    }
}
