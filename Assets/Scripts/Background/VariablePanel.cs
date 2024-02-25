using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablePanel : MonoBehaviour
{

    [SerializeField] private SpriteRenderer AltPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( gameObject.transform.position.y < 0)
        {
            AltPanel.sortingOrder = -1;
        }
        else
        {
            AltPanel.sortingOrder = -3;
        }
    }
}
