using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + 4 * horizontal * Time.deltaTime;
        position.y = position.y + 4 * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    void FixedUpdate()
    {
        //Get current position and set to new position
        Vector2 position = rigidbody2d.position;
        position.x = position.x + 4 * horizontal * Time.deltaTime;
        position.y = position.y + 4 * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}
