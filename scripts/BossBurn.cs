using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBurn : MonoBehaviour
{
    private bool CanBurn = true;   //for the flame

    private void Start()
    {
        CanBurn = true;
    }
    private void OnParticleCollision(GameObject other)    //function for flame particle collision with boss
    {
        gameObject.SendMessageUpwards("Hit");      //sends message of boss hit to parent
        if(CanBurn == true)                        //for the flame
        {
            CanBurn = false;
            SaveScript.BossHealth -= 1;
            SaveScript.Score += 1000;
        }
    }

}
