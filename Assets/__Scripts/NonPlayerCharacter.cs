using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public int Health = 2;

    public float displayTime = 4.0f;
    public GameObject dialogBox;
    float timerDisplay;

    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    Animator animator;
    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    void Start()
    {
        animator = GetComponent<Animator>();

        dialogBox.SetActive(false);
        timerDisplay = -1.0f;

        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }

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
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
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


    }
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }

}
