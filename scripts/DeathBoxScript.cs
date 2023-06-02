using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))      //checks if player falls down from height
        {
            SaveScript.HealthAmt = 0;                 //player dies
        }
    }
}
