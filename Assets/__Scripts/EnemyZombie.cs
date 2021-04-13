using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : EnemyController
{
    public float startWaitTime;
    private float waitTime;

    public Transform movPos; //the position gonna move
    public Transform lestDownPos;
    public Transform rightUpPos; //range for move


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        movPos.position = GetRandomPos();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movPos.position, speed * Time.deltaTime);

        //compare the distance  between before and after
        if (Vector2.Distance(transform.position, movPos.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                movPos.position = GetRandomPos();
                waitTime = startWaitTime;
            }

            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector2 GetRandomPos()
    {
        Vector2 rndPos = new Vector2(Random.Range(lestDownPos.position.x, rightUpPos.position.x), Random.Range(lestDownPos.position.y, rightUpPos.position.y));

        return rndPos;

    }
}
