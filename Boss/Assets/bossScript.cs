using System;
using System.Collections;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    public float hp = 100;
    public Transform[] spots;
    public float speed;
    public GameObject projectile;
    GameObject player;
    public Transform[] holes;
    Vector3 playerPos;
    public bool vulnerable;

    public GameObject explosion;
    public Sprite[] sprites;
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

    IEnumerator boss()
    {
        while (true)
        {
            //FIRST ATTACK

            while (transform.position.x != player.transform.position.x)
            {
                
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed);

                yield return null;
            }

            Debug.Log($"[Enemy] -> Hp: {hp}");

            //transform.localScale = new Vector2(-1, 1);

            yield return new WaitForSeconds(3);

            // int i = 0;
            // while (i < 6)
            // {

            //     GameObject bullet = (GameObject)Instantiate(projectile, holes[Random.Range(0, 2)].position, Quaternion.identity);
            //     bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * 5;

            //     i++;
            //     yield return new WaitForSeconds(.7f);
            // }


            //SECOND ATTACK
            // GetComponent<Rigidbody2D>().isKinematic = true;
            // while (transform.position != spots[1].position)
            // {
            //     Debug.Log("SECOND ATTACK");
            //     Debug.Log("Spot 1: " + spots[1].tag);
            //     transform.position = Vector2.MoveTowards(transform.position, spots[1].position, speed);

            //     yield return null;
            // }

            //playerPos = player.transform.position;

            // yield return new WaitForSeconds(1f);
            // GetComponent<Rigidbody2D>().isKinematic = false;

            // while (transform.position.x != playerPos.x)
            // {

            //     transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPos.x, transform.position.y), speed);

            //     yield return null;
            // }

            // yield return new WaitForSeconds(2);

            // this.tag = "Untagged";
            // GetComponent<SpriteRenderer>().sprite = sprites[1];
            
            // yield return new WaitForSeconds(4);
            // this.tag = "Deadly";
            // GetComponent<SpriteRenderer>().sprite = sprites[0];
            // vulnerable = false;

            //THIRD ATTACK
            // Transform temp;
            // if (transform.position.x > player.transform.position.x)
            //     temp = spots[1];
            // else
            //     temp = spots[0];

            // while (transform.position.x != temp.position.x)
            // {

            //     transform.position = Vector2.MoveTowards(transform.position, new Vector2(temp.position.x, transform.position.y), speed);

            //     yield return null;
            // }


        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player" && vulnerable)
        {
            hp -= 30;
            vulnerable = false;
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
    }

}