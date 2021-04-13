using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCha : MonoBehaviour
{

    public GameObject ch1, ch2;
    int whichOn = 1;

    // Start is called before the first frame update
    void Start()
    {
        ch1.gameObject.SetActive(true);
        ch2.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //use to switch two character by cliking the button
    public void SwitchCharacter()
    {
        switch (whichOn)
        {
            case 1:
                whichOn = 2;
                ch1.gameObject.SetActive(false );
                ch2.gameObject.SetActive(true);
                break;

            case 2:
                whichOn = 1;
                ch1.gameObject.SetActive(true);
                ch2.gameObject.SetActive(false);
                break;

        }
    }
}
