using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    public GameObject newPlayerPrefab;
    void OnTriggerStay2D(Collider2D collision)
    {
        IDamagable obj = collision.gameObject.GetComponent<IDamagable>();

        if (obj != null)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                GameObject newPlayer = Instantiate(newPlayerPrefab, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
        }
    }
}
