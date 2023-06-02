using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDeath : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)    //function for flame particle collision with minion
    {
        gameObject.SendMessageUpwards("MinionBurned");      //sends message of minion death to parent
    }

}
