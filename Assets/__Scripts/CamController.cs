using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamController : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    public GameObject[] players;
    GameObject currentPlayer;

    CinemachineConfiner aCameraConfiner;

    public static bool isCamConfigPresent;
    private void Awake()
    {
        currentPlayer = Instantiate(players[0], this.gameObject.transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {

        cam = GetComponent<CinemachineVirtualCamera>();

        cam.Follow = currentPlayer.transform;

        aCameraConfiner = GameObject.FindGameObjectWithTag("CameraConfig").GetComponent<CinemachineConfiner>();

        if (!isCamConfigPresent)
        {
            isCamConfigPresent = true;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer != null)
        {
            cam.Follow = currentPlayer.transform;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentPlayer = Instantiate(players[1], this.gameObject.transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentPlayer = Instantiate(players[0], this.gameObject.transform.position, Quaternion.identity);
        }

        if (aCameraConfiner.m_BoundingShape2D == null)
        {
            //Update Camera Confiner
            aCameraConfiner.InvalidatePathCache();
            aCameraConfiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("Confiner").GetComponent<Collider2D>();
        }
    }


}
