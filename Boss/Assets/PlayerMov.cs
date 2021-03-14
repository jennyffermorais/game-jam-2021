using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpHeight = 18f;
    public float walkSpeed = 5f;
    public bool canJump;
    private SpriteRenderer sprite;
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer> ();
        anim =  GetComponent<Animator> ();

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
        var move = Input.GetAxis("Horizontal");
        if ((move > 0f && sprite.flipX) || (move < 0f && !sprite.flipX)) Flip();

        if (move > 0)
        {
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
           
        }
        else if (move < 0)
        {
            
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
        }
    }

    void Flip() {
        sprite.flipX = !sprite.flipX;
    }


    // IEnumerator Damage() {
    //     for(float i = 0f; i < if; i += 0.1f) {
    //         sprite.enabled = false;
    //         yield return new WaitForSeconds(0.1f);
    //         sprite.enabled = true;
    //         yield return new WaitForSeconds (0.1f);
    //     }
    //     invunerable = false;
    // }
    // public void DamagePlayer() {
    //     invunerable = true;
    //     health--;
    //     StartCoroutine(Damage ());

    //     if (health < 1) {
    //         Debug.Log("Morreu");
    //     }
    // }

}