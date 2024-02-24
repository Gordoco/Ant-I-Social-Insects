using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private Camera cam;


    //The dimensions of a background panel in world units
    [SerializeField] private float panelHeight = 16.0f;
    private float halfHeight;
    [SerializeField] private float panelWidth = 32.0f;
    private float halfWidth;


    //Odd index Panels start on the second row
    //higher number panels start thurther to the right
    [SerializeField] GameObject[] panels;




    // Start is called before the first frame update
    void Start()
    {
        
        halfWidth = panelWidth / 2;
        halfHeight = panelHeight / 2;
    }

    // Update is called once per frame
    void Update()
    {
        

        Vector3 camPosition = cam.gameObject.transform.position;

        Vector3Int gridCamPosition = new Vector3Int(Mathf.RoundToInt( camPosition.x / panelWidth), Mathf.RoundToInt(camPosition.y / panelHeight), Mathf.RoundToInt(camPosition.z));



        int xflip = 1 - 2 * (Mathf.Abs(gridCamPosition.x) % 2);

        int yflip = 1 - 2 * (Mathf.Abs(gridCamPosition.y) % 2);


        panels[0].transform.position = new Vector3(gridCamPosition.x * panelWidth - (xflip * halfWidth), 
            gridCamPosition.y * panelHeight - (yflip * halfHeight), panels[0].transform.position.z);

        panels[1].transform.position = new Vector3(gridCamPosition.x * panelWidth - (xflip * halfWidth),
            gridCamPosition.y * panelHeight + (yflip * halfHeight), panels[1].transform.position.z);

        panels[2].transform.position = new Vector3(gridCamPosition.x * panelWidth + (xflip * halfWidth),
            gridCamPosition.y * panelHeight - (yflip * halfHeight), panels[2].transform.position.z);

        panels[3].transform.position = new Vector3(gridCamPosition.x * panelWidth + (xflip * halfWidth), 
            gridCamPosition.y * panelHeight + (yflip * halfHeight), panels[3].transform.position.z);


        

    }
}
