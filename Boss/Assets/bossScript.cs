using System;
using System.Collections;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    public float hp = 50;
    public Transform[] spots;
    public float speed;
    public GameObject projectile;
    GameObject player;
    public Transform[] holes;
    Vector3 playerPos;
    public bool vulnerable;

    public GameObject explosion;
    public Sprite[] sprites;
    private SpriteRenderer Render;

    bool dead;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Render = GetComponent<SpriteRenderer>();
        StartCoroutine("boss");
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && !dead)
        {
            dead = true;
            Render.color = Color.grey;
            StopCoroutine("boss");
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }



    void Flip() {
        Render.flipX = !Render.flipX;
    }

    IEnumerator boss()
    {
        while (true)
        {
            var vulnerableTime = DateTime.Now;
            Debug.Log($"[Enemy] -> vulnerable: {vulnerable}");
            vulnerable = false;
            Render.sprite = sprites[1];
            while (transform.position.x != player.transform.position.x && (DateTime.Now - vulnerableTime).Seconds < 5)
            {
                var move = player.transform.position.x - transform.position.x;

                if((move > 0f && !Render.flipX) || (move < 0f && Render.flipX)) Flip();

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed);

                yield return null;
            }

            Debug.Log($"[Enemy] -> Hp: {hp}");
            vulnerable = true;
            Render.sprite = sprites[0];

            Debug.Log($"[Enemy] -> vulnerable: {vulnerable}");

            while((DateTime.Now - vulnerableTime).Seconds <= 6 && vulnerable){
                yield return null;
            }

            yield return new WaitForSeconds(1);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player" && vulnerable)
        {
            hp -= 20;
            vulnerable = false;
            Render.sprite = sprites[1];
        }
    }

}