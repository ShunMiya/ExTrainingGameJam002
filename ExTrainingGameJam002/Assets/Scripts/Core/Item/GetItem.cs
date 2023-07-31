using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    public int num;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch(num)
            {
                case 0:
                    Debug.Log("key");
                    break;
                case 1:
                    Debug.Log("poision");
                    break;
            }
            Destroy(gameObject);
        }
    }
}