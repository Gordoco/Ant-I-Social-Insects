using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenCameraMover : MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private float camSpeed = 12.0f;

    [SerializeField] private GameObject[] menuLocations = new GameObject[3];
    [SerializeField] private GameObject currMenu;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, currMenu.transform.position, camSpeed);
    }


    //The destination parameter cooresponds with the index of a game object in the menuLocations array
    public void MoveToLocation(int destination)
    {
        currMenu = menuLocations[destination];
    }

}
