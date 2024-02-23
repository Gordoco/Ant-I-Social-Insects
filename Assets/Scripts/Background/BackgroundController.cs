using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Stuff;
    [SerializeField] private Camera cam;

    private Vector2Int cameraResolution;
    [SerializeField] private Vector2Int panelResolution = new Vector2Int(1024,1024);

    //The distance from one panel to the next
    private float panelDistance = 16.0f;


    //Odd index Panels start on the second row
    //higher number panels start thurther to the right
    [SerializeField] GameObject[] panels;
    private Vector2 cameraDimensions;

    private bool oddOntop = true;



    // Start is called before the first frame update
    void Start()
    {
        cameraDimensions = new Vector2(cam.pixelRect.width, cam.pixelRect.height) / panels[0].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        Debug.Log(cameraDimensions);


        Debug.Log("Upper edge: " + (panels[0].transform.position.y + panelDistance / 2));
        Debug.Log("Camera bound: " + (cam.transform.position.y - cameraDimensions.y));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPosition = cam.gameObject.transform.position;

        GameObject bottomPanel = panels[0];
        if (!oddOntop)
        {
            bottomPanel = panels[1];
        }

        //Moving bottom panels up
        if (bottomPanel.transform.position.y + panelDistance/2 <= camPosition.y - cameraDimensions.y)
        {
            if (oddOntop)
            {
                panels[0].transform.position = new Vector3(panels[0].transform.position.x, panels[1].transform.position.y + panelDistance, panels[0].transform.position.z);
                panels[2].transform.position = new Vector3(panels[2].transform.position.x, panels[3].transform.position.y + panelDistance, panels[2].transform.position.z);
                oddOntop = false;
            }
            else
            {
                panels[1].transform.position = new Vector3(panels[1].transform.position.x, panels[0].transform.position.y + panelDistance, panels[1].transform.position.z);
                panels[2].transform.position = new Vector3(panels[3].transform.position.x, panels[2].transform.position.y + panelDistance, panels[3].transform.position.z);
                oddOntop = true;
            }
        }

    }
}
