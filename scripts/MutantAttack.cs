using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantAttack : MonoBehaviour
{
    private bool canHit = true;
    private void OnTriggerEnter(Collider other)  //to check trigger of minion's hand with player
    {
        if (SaveScript.PlayerDead == false)
        {
            if (canHit == true)
            {
                canHit = false;
                if (other.gameObject.CompareTag("Player"))
                {
                    Debug.Log("hit");
                    SaveScript.HealthAmt -= 1;    //decrease the health by 1
                    other.transform.gameObject.SendMessage("GetHit");   //sends message to the player using GetHit function present in playermove script
                    StartCoroutine(delayHit());
                }
            }
           
        }
    }

    IEnumerator delayHit()
    {
        yield return new WaitForSeconds(0.2f);
        canHit = true;
    }
}

