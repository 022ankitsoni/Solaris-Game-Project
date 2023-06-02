using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMinionSounds : MonoBehaviour
{
    private AudioSource MinionAudio;     //to store audio clip of minion
    [SerializeField] AudioClip Sound1;   //to store different audio clips
    [SerializeField] AudioClip Sound2;   //to store different audio clips
    [SerializeField] AudioClip Sound3;   //to store different audio clips
    private int Selection = 1;            //variable for randomizing
    private bool Randomizer = false;     //variable for switch on randomizing
    private bool RandomSoundDelay = false;      //for delaying the sound of minions
    [SerializeField] float DelayTime = 3.0f;    //for changing the delay time(variable delay time) 

    // Start is called before the first frame update
    void Start()
    {
        MinionAudio = GetComponent<AudioSource>();    //getting audio component from unity
        Randomizer = true;                            //in beginning it is true
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.PlayerDead == false)
        {
            if (Randomizer == true)
            {
                Randomizer = false;
                Selection = Random.Range(1, 4);        //getting random numbers between 1 and 3

                if (Selection == 1)                     //starting sound1
                {
                    MinionAudio.clip = Sound1;
                    MinionAudio.Play();
                }
                else if (Selection == 2)                     //starting sound2
                {
                    MinionAudio.clip = Sound2;
                    MinionAudio.Play();
                }
                else if (Selection == 3)                     //starting sound3
                {
                    MinionAudio.clip = Sound3;
                    MinionAudio.Play();
                }

                if (RandomSoundDelay == false)
                {
                    RandomSoundDelay = true;
                    StartCoroutine(NewSound());     //for pausing the script for some time
                }
            }
        }
    }

    IEnumerator NewSound()
    {
        yield return new WaitForSeconds(DelayTime);    //waits for 3 seconds
        RandomSoundDelay = false;
        Randomizer = true;                      //to start again whole process of this script
    }
}
