using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDeath : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)    //function for barrel explosion particle collision with minion
    {
        gameObject.SendMessageUpwards("MinionBarrelExplode");      //sends message of minion death to parent
    }

}
