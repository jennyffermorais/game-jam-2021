using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpHeight = 24f;
    public float walkSpeed = 5f;
    public bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        JumpFunction();
        WalkFunction();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            canJump = true;
        }
    }

    void JumpFunction()
    {
        if (Input.GetButtonDown("Jump") && canJump)
        {
            // Modo 1
            rb.velocity += new Vector2(0f, jumpHeight);

            // Modo 2
            //rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);

            canJump = false;
        }
    }
    void WalkFunction()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
        }
    }
}