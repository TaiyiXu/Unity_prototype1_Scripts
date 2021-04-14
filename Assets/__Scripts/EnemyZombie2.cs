using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie2 : EnemyController
{
    //chasing speed
    public float speedZ;

    //the range that enemy will start chase
    public float radius;


    private Transform playerTranform;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerTranform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        //check the position of enmey and player 
        if (playerTranform != null)
        {
            float distance = (transform.position - playerTranform.position).sqrMagnitude; //distance btwn player and enemy
            if (distance < radius)
            {
                //if the player are inside the attacking radius, enemy will start chase
                transform.position = Vector2.MoveTowards(transform.position, playerTranform.position, speedZ * Time.deltaTime);
            }


        }
    }
}
