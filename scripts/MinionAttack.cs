using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAttack : MonoBehaviour
{
    private AudioSource MinionAudio;     //variable to store audio clip of minion attack
    [Tooltip("1=Running,2=Crawl,3=Drag")]    //for showing suggestions when cursor is on miniontype option
    [SerializeField] int MinionType = 1;    //variable for storing type of minion among different types

    private void Start()
    {
        MinionAudio = GetComponentInParent<AudioSource>();     //getting audio component in parent object
    }

    [SerializeField] int DamageAmt = 1;     //variable for storing damage capacity for different types of minions
    private void OnTriggerEnter(Collider other)  //to check trigger of minion's hand with player
    {
        if (SaveScript.PlayerDead == false)
        {
            if (MinionType == 1)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    SaveScript.HealthAmt -= DamageAmt;    //decrease the health by 1
                    other.transform.gameObject.SendMessage("GetHit");   //sends message to the player using GetHit function present in playermove script
                    MinionAudio.Play();       //play the minion sound of attack
                }
            }
            else if (MinionType == 2)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    SaveScript.HealthAmt -= DamageAmt;    //decrease the health by 1
                    MinionAudio.Play();       //play the minion sound of attack
                }
            }
            else if (MinionType == 3)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    SaveScript.HealthAmt -= DamageAmt;    //decrease the health by 1
                    MinionAudio.Play();       //play the minion sound of attack
                }
            }
        }
    }
}
