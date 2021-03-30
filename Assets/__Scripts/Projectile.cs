using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamagable
{
    Rigidbody2D rigidbody2d;

    public int Health { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        Health = 1;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int damageAmount)
    {
        if (damageAmount == -1)
        {
            Destroy(this.gameObject);
        }
    }
}
