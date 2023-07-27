using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        if (Input.GetKey("up"))
        {
            position.y += speed;
        }

        else if (Input.GetKey("down"))
        {
            position.y -= speed;
        }

        else if (Input.GetKey("left"))
        {
            position.x -= speed;
        }

        else if (Input.GetKey("right"))
        {
            position.x += speed;
        }

        transform.position = position;

    }
}
