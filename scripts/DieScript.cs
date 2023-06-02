using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{
    [SerializeField] Material DissolveMat;      //to dissolve the material of minion after it is died

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = DissolveMat;     //current material will change to dissolve material
        GetComponent<SpawnEffect>().enabled = true;          //to start the dissolve effect
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
