using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour
{
    public int Health;

    public float speed;
    public float timeBetweenMove;
    protected float timeBetweenMoveCounter;

    protected bool isMoving;
    protected Vector2 movingDirection;

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected Rigidbody2D rbody2D;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rbody2D = GetComponent<Rigidbody2D>();
        timeBetweenMoveCounter = timeBetweenMove;

    }

    protected virtual void FixedUpdate()
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

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        IDamagable obj = other.gameObject.GetComponent<IDamagable>();
        APlayer playerObj = other.gameObject.GetComponent<APlayer>();

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

    protected IEnumerator DamageEffectSequence(SpriteRenderer sr, Color dmgColor, float duration)
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
