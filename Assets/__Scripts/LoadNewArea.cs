using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LoadNewArea : MonoBehaviour
{

    CinemachineConfiner cConfiner;

    public string sceneName;
    public GameObject startPoint;

    void Start()
    {
        //Update Camera Confiner
        cConfiner = GameObject.FindGameObjectWithTag("CameraConfig").GetComponent<CinemachineConfiner>();
        cConfiner.InvalidatePathCache();
        cConfiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("Confiner").GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        APlayer obj = collision.gameObject.GetComponent<APlayer>();
        if (obj != null)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
