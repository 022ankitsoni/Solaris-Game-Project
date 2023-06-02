using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBarrel : MonoBehaviour
{
    [SerializeField] GameObject Explosion;    //variable for explosion of barrels
    [SerializeField] bool SpawnDestroyer = false;    //to destroy spawning place when barrel explodes
   
    public void Explode()
    {
        Instantiate(Explosion, this.transform.position, this.transform.rotation);   //initiate the explosion animation
        if(SpawnDestroyer == true)
        {
            GetComponent<DestroySpawn>().enabled = true;
        }
        Destroy(gameObject, 0.1f);
    }
}
