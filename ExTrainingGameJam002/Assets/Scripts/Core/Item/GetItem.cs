using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Player;
using StageSystem;

public class GetItem : MonoBehaviour
{
    public int num;
    private AreaClear areaClear;
    private GetHolyWater getHolyWater;

    private void Start()
    {
        areaClear = FindObjectOfType<AreaClear>();
        getHolyWater = FindObjectOfType<GetHolyWater>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (num)
            {
                case 0:
                    areaClear.HaveKeyUpdate();
                    break;
                case 1:
                    getHolyWater.UndetectableTime();
                    break;
            }
            Destroy(gameObject);
        }
    }
}