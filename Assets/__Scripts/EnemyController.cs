using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int Health = 3;


    public float speed = 3.0f;
    public float timeBetweenMove;
    protected float timeBetweenMoveCounter;

    protected bool isMoving;
    protected Vector2 movingDirection;

    public float changeTime = 3.0f;

    protected SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rbody2D;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rbody2D = GetComponent<Rigidbody2D>();
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
        if (!isMoving)
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            rbody2D.velocity = Vector2.zero;
            if (timeBetweenMoveCounter < 0)
            {
                //Add some randomness
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
                isMoving = true;
                movingDirection = new Vector2(Random.Range(-1f, 1f) * speed, Random.Range(-1f, 1f) * speed);
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            rbody2D.velocity = movingDirection;
            if (timeBetweenMoveCounter < 0)
            {
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
                isMoving = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamagable obj = other.gameObject.GetComponent<IDamagable>();
        Player playerObj = other.gameObject.GetComponent<Player>();

        if (obj != null && playerObj == null)
        {
            StartCoroutine(DamageEffectSequence(spriteRenderer, Color.red, 0.1f));
            Health -= 1;
            obj.Damage(-1);

        }
        else if (playerObj != null)
        {
            obj.Damage(-1);
        }
    }
    IEnumerator DamageEffectSequence(SpriteRenderer sr, Color dmgColor, float duration)
    {
        // save origin color
        Color originColor = sr.color;

        // tint the sprite with damage color
        sr.color = dmgColor;

        // lerp animation with given duration in seconds
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            sr.color = Color.Lerp(dmgColor, originColor, t);

            yield return null;
        }

        // restore origin color
        sr.color = originColor;
    }
}
