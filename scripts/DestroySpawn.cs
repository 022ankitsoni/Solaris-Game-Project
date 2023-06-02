using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpawn : MonoBehaviour
{
    [SerializeField] GameObject SpawnGas;       //for storing spawn place to destroy it
    // Start is called before the first frame update
    void Start()
    {
        Destroy(SpawnGas, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
