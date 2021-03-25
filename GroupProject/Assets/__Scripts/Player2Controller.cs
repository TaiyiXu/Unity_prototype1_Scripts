using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player2Controller : PlayerController
{

    EdgeCollider2D edgeCollider;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        healthText = canvasObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.enabled = !edgeCollider.enabled;
    }

    // Update is called once per frame
    protected override void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //Get mouse position, transfer it to look direction
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = worldMousePos - transform.position;
        lookDirection.Normalize();

        if (lookDirection.x > 0)
            transform.localScale = new Vector3(-1.5f, this.transform.localScale.y, this.transform.localScale.z);
        else if (lookDirection.x < 0)
            transform.localScale = new Vector3(1.5f, this.transform.localScale.y, this.transform.localScale.z);
        if (Mathf.Abs(horizontal) > Mathf.Epsilon)
        { animator.SetInteger("AnimState", 2); }
        else
        { animator.SetInteger("AnimState", 0); }


        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            Destroy(this.gameObject);
            Destroy(this.canvasObj.gameObject);
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            //Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            edgeCollider.enabled = !edgeCollider.enabled;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Launch();
        }

        if (Input.GetKeyDown("space"))
        {

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
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }


}
