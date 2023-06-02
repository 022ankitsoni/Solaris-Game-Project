using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;    //there is no cursor in beginning
    }

    // Update is called once per frame
    void Update()
    {
       // this.transform.position = Input.mousePosition;  //crosshair is mathched with mousecursor
    }
}
