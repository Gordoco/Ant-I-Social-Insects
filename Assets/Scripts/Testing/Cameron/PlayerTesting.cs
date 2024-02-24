using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTesting : MonoBehaviour
{

    float speed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3();


        if (Input.GetKey(KeyCode.W))
        {
            direction.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction.y -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1;
        }

        direction.Normalize();

        gameObject.transform.position = gameObject.transform.position + (direction * speed * Time.deltaTime);

    }
}
