using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultTime : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI textMeshPro;

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }
}