using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player playerObj = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerObj.transform.position = GameObject.FindGameObjectWithTag("StartPoint").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}