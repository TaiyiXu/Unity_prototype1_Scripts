using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float time;

    private Animator anim;
    private PolygonCollider2D coll2D;



    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();


    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }


    void Attack()
    {
        if (Input.GetButtonDown("Attack"))//when you press keyboard 'j'
        {
            coll2D.enabled = true;
            anim.SetTrigger("Attack");
            StartCoroutine(disableHitBox());

        }
    }


    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        coll2D.enabled = false;
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }*/
}

