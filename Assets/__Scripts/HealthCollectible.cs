using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();

        if (p.tag == "Player" && p.Health < p.maxHealth)
        {
            IDamagable p1 = collision.GetComponent<IDamagable>();
            p1.Damage(1);
            Destroy(gameObject);
        }

    }
}
