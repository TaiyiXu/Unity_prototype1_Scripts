using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scavenger : Player, IDamagable
{
    public GameObject blood;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && StaminaBar.instance.getCurrentStamina()!=0)
        {
            Launch();
            StaminaBar.instance.useStamina(1000);
        }

        //Player Sprint
        if (Input.GetKey(KeyCode.LeftShift) && StaminaBar.instance.getCurrentStamina() != 0)
        {
            StaminaBar.instance.useStamina(10);
            speed = boostSpeed;
        }
        else
        {
            speed = normalSpeed;
        }


        //Gameover
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        animator.SetFloat("Move X", lookDirection.x);

    }

    //Launch projectile
    protected void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

    }
    public void Damage(int damageAmount)
    {
        if (damageAmount < 0)
        {
            animator.SetTrigger("Got Hurt");
            Instantiate(blood, transform.position, Quaternion.identity);
            StartCoroutine(Flash(flashSpeed));
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + damageAmount, 0, maxHealth);
        SetHealthText();
        Debug.Log(currentHealth + "/" + maxHealth);


    }
}
