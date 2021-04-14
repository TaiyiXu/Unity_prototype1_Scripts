using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bandit : Player, IDamagable
{

    public GameObject blood;

    EdgeCollider2D edgeCollider;
    bool isDead;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        isDead = false;

        //Get Sword Collider and disable it
        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.enabled = !edgeCollider.enabled;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!isDead)
        {
            if (lookDirection.x > 0)
                transform.localScale = new Vector3(-1.5f, this.transform.localScale.y, this.transform.localScale.z);
            else if (lookDirection.x < 0)
                transform.localScale = new Vector3(1.5f, this.transform.localScale.y, this.transform.localScale.z);
        }
        if (Mathf.Abs(horizontal) > Mathf.Epsilon)
        { animator.SetInteger("AnimState", 2); }
        else
        { animator.SetInteger("AnimState", 0); }

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            Instantiate(blood, transform.position, Quaternion.identity);
            isDead = true;
            //Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0)&& StaminaBar.instance.getCurrentStamina()!=0)
        {
            animator.SetTrigger("Attack");
            Instantiate(blood, transform.position, Quaternion.identity);
            edgeCollider.enabled = !edgeCollider.enabled;
            edgeCollider.enabled = !edgeCollider.enabled;
            StaminaBar.instance.useStamina(1000);

        }

        if (Input.GetMouseButtonDown(1))
        {
            Launch();


        }

    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void Damage(int damageAmount)
    {
        if (damageAmount < 0)
        {
            if (isInvincible)
                return;
            animator.SetTrigger("Hurt");
            StartCoroutine(Flash(flashSpeed));
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + damageAmount, 0, maxHealth);
        SetHealthText();
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
