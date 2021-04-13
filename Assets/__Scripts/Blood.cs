using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public float timeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);//set the time to end blooding
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
