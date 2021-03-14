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
    //private SpriteRenderer sprite;

    bool dead;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("boss");
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && !dead)
        {
            dead = true;
            GetComponent<SpriteRenderer>().color = Color.grey;
            StopCoroutine("boss");
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }



    void Flip() {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }

    IEnumerator boss()
    {
        while (true)
        {
            var vulnerableTime = DateTime.Now;
            Debug.Log($"[Enemy] -> vulnerable: {vulnerable}");
            vulnerable = false;
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            while (transform.position.x != player.transform.position.x && (DateTime.Now - vulnerableTime).Seconds < 5)
            {
                var move = player.transform.position.x - transform.position.x;

                //if((move > 0f && sprite.flipX) || (move < 0f && !sprite.flipX)) Flip();

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed);

                yield return null;
            }

            Flip();

            Debug.Log($"[Enemy] -> Hp: {hp}");
            vulnerable = true;
            GetComponent<SpriteRenderer>().sprite = sprites[0];

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
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
    }

}