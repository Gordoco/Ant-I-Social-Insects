using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTether : MonoBehaviour
{
    [SerializeField] private float distThreshold = 0;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player) Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) return;
        float dist = (player.transform.position.y - gameObject.transform.position.y);
        Debug.Log(dist);
        if (Mathf.Abs(dist) > distThreshold)
        {
            transform.position += new Vector3(0, player.GetComponent<Rigidbody2D>().velocity.y * Time.deltaTime, 0);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, Mathf.Infinity), transform.position.z);
        }
    }
}
