using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CamController : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    public GameObject[] players;
    GameObject currentPlayer;
    CinemachineConfiner cConfiner;
    public GameObject cameraConfiner;

    //Player Existence
    protected static bool camExists;
    private void Awake()
    {

        currentPlayer = Instantiate(players[0], gameObject.transform.position, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        cConfiner = GetComponent<CinemachineConfiner>();
        cam = GetComponent<CinemachineVirtualCamera>();
        cam.Follow = currentPlayer.transform;

        if (!camExists)
        {
            camExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

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
            currentPlayer = Instantiate(players[1], gameObject.transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentPlayer = Instantiate(players[0], gameObject.transform.position, Quaternion.identity);
        }
    }


}
