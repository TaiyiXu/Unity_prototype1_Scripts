using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int Health = 3;


    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    Animator animator;
    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if (Health <= 0)
        {
            Destroy(gameObject);
        }



    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
        }

        rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        Projectile bullet = other.gameObject.GetComponent<Projectile>();
        Player2Controller player2 = other.gameObject.GetComponent<Player2Controller>();

        if (player != null)
        {
            player.ChangeHealth(-1);

        }


        if (bullet != null)
        {
            Destroy(bullet.gameObject);
            Health -= 1;

        }

        if(player2 != null)
        {
            Health -= 1;
        }
    }
}
