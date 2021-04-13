using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : AEnemy
{
    protected override void Update()
    {
        animator.SetFloat("Move X", movingDirection.x);
        base.Update();
    }

    protected override void OnCollisionEnter2D(Collision2D other)
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
            animator.SetTrigger("IsAttack");
            animator.SetFloat("Attack X", movingDirection.x);
            obj.Damage(-1);
        }

    }

}
