using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    public string sceneName;

    public static LoadNewArea instance;


    void OnTriggerEnter2D(Collider2D collision)
    {
        Player obj = collision.gameObject.GetComponent<Player>();
        if (obj != null)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public void setLevel2()
    {
        sceneName = "Level2";
    }
}
