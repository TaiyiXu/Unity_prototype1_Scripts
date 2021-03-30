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
            p.Health = 1;
            Destroy(gameObject);
        }

    }
}
