using System;
using System.Collections;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    public float hp = 60;
    public Transform[] spots;
    public float speed;
    public GameObject projectile;
    PlayerMov player;
    public Transform[] holes;
    Vector3 playerPos;
    public States vulnerable;

    public GameObject explosion;
    public Sprite[] sprites;
    private SpriteRenderer Render;

    public bool dead { get; private set; }


    // Use this for initialization
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMov>();
        Render = GetComponent<SpriteRenderer>();
        StartCoroutine("boss");
    }

    // Update is called once per frame
    public void Update()
    {
        if (hp <= 0 && !dead)
        {
            dead = true;
        }
    }

    private void Flip()
    {
        Render.flipX = !Render.flipX;
    }

    IEnumerator boss()
    {
        while (true)
        {
            var vulnerableTime = DateTime.Now;
            vulnerable = States.BLOCKED;
            Render.sprite = sprites[1];
            while (transform.position.x != player.transform.position.x && (DateTime.Now - vulnerableTime).Seconds < 5 && !player.dead)
            {
                var move = player.transform.position.x - transform.position.x;
                if ((move > 0f && !Render.flipX) || (move < 0f && Render.flipX)) Flip();
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed);
                yield return null;
            }

            vulnerable = States.DAMAGE;
            Render.sprite = sprites[0];

            while ((DateTime.Now - vulnerableTime).Seconds <= 6 && vulnerable == States.DAMAGE)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1);
        }

    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player" && vulnerable == States.BLOCKED)
        {
            Debug.Log("[Enemy] Attack");
            player.DamagePlayer(this);
        }

        if (col.collider.tag == "Player" && vulnerable == States.DAMAGE)
        {
            hp -= 20;
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        if (hp >= 1)
        {
            Render.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            Render.color = Color.white;
            vulnerable = States.STAGE;
        }
        else if (hp < 1)
        {
            Render.color = Color.grey;
            StopCoroutine("boss");
        }
    }
}

public enum States
{
    DAMAGE, STAGE, BLOCKED
}