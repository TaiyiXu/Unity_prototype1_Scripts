using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("QuestManager"));
        Destroy(GameObject.FindGameObjectWithTag("CameraConfig"));
        Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        Debug.Log("Game Over");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
