using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMov : MonoBehaviour
{
    public float HP = 60;
    public Rigidbody2D rb;
    public float jumpHeight = 18f;
    public float walkSpeed = 5f;
    public bool canJump;
    private SpriteRenderer Render;
    private Animator anim;

    private DateTime? DamageDelay = null;

    private bool invulnerable = false;

    bossScript boss;
    public bool dead { get; private set; }

    public void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Deadly").GetComponent<bossScript>();
        rb = GetComponent<Rigidbody2D>();
        Render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //StartCoroutine("player");

    }

    public void Update()
    {
        if (!dead && !boss.dead)
        {
            JumpFunction();
            WalkFunction();
        }
        Damage();

        if (DamageDelay.HasValue) SetVulnerability();

        // if (HP <= 0 && !dead)
        // {
        //     dead = true;

        // }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            canJump = true;
        }
    }

    private void JumpFunction()
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
    private void WalkFunction()
    {
        var move = Input.GetAxis("Horizontal");
        if ((move > 0f && Render.flipX) || (move < 0f && !Render.flipX)) Flip();

        if (move > 0)
        {
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);

        }
        else if (move < 0)
        {

            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        Render.flipX = !Render.flipX;
    }


    public void DamagePlayer(bossScript boss)
    {
        if (DamageDelay == null) DamageDelay = DateTime.Now;

        if (!invulnerable && !dead) //tá vulnerável e tá vivo
        {
            HP -= 20;
            StartCoroutine(Damage());
            Debug.Log($"[Hero]: Damage HP: {HP}");
        }
    }

    IEnumerator Damage()
    {
        if (HP >= 1)
        {
            Render.enabled = false;
            yield return new WaitForSeconds(0.1f);
            Render.enabled = true;

        }

        else if (HP < 1)
        {
            Render.color = Color.grey;
            dead = true;
            //inserir aqui a animação de morte
            Invoke("ReloadLevel", 3f);

        }
    }

    private void SetVulnerability()
    {
        var timeNow = DateTime.Now;
        if (DamageDelay.HasValue && (timeNow - DamageDelay.Value).Seconds < 3)
        {
            invulnerable = true;
        }
        else
        {
            invulnerable = false;
            DamageDelay = null;
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}