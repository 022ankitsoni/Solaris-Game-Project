using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    private GameObject PlayerTarget;       //to store the main player
    [SerializeField] float RotationSpeed = 2.0f;   //for rotation of boss to face the player
    [SerializeField] GameObject SpawnPlace1, SpawnPlace2, SpawnPlace3, SpawnPlace4, SpawnPlace5, SpawnPlace6, SpawnPlace7, SpawnPlace8, SpawnPlace9, SpawnPlace10;        //for storing spawn places to change the position of boss
    private Animator Anim;                   //to get the animator component
    private int SpawnID = 0;                 //to store a particular spawn place
    [SerializeField] float DestroyTime = 1.0f;    //to destroy the boss temporarily to reappear at different spawn place
    [SerializeField] GameObject BossVampire;     //to store the vampire boss

    [SerializeField] Transform BlobSpawn;      //for storing blob spawn for attacking the player by boss
    [SerializeField] GameObject Blob;          //for storing the blob which is the fire coming from the mouth of boss
    private bool Attack = false;               //to check if boss can attack the blob or not
   // public bool CanHitBoss = false;            //to stop again and again hitting of boss before its vanishing
    [SerializeField] float AttackTime = 3.0f;         //to ensure how quickly the boss can attack after the first attack(time duration between two attacks) 
    private AudioSource BossAudio;             //for sound of boss
    [SerializeField] AudioClip SpawnSound;      //storing spawn sound
    [SerializeField] AudioClip HitSound;        //storing hit sound when player hits the boss
  //  public GameObject canhitboss;
   
    // Start is called before the first frame update
    void Start()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");   //find player to store 
        Anim = GetComponent<Animator>();
        BossAudio = GetComponent<AudioSource>();      //getting audio source component
    }

    // Update is called once per frame
    void Update()
    {
       
        if (SaveScript.PlayerDead == false)
        {
            Vector3 Pos = (PlayerTarget.transform.position - transform.position).normalized;   //to calculate distance between palyer and boss when nav agent is switched off for rotating the minion
            Quaternion PosRotation = Quaternion.LookRotation(new Vector3(Pos.x, 0, Pos.z));    //rotate the boss about x and z axis
            transform.rotation = Quaternion.Slerp(transform.rotation, PosRotation, Time.deltaTime * RotationSpeed);  //to rotate the boss slowly towards the player


            if (Attack == false)           //to check if boss can attack or not
            {
                Attack = true;
                StartCoroutine(AttackPlayer());    //pause the script for sometime 
            }
        }
    }

    public void Hit()
    {
        //CanHitBoss = false;
        Anim.SetTrigger("Spin");      //start the spin animation when boss gets hits
        BossAudio.clip = HitSound;    //storing the hit sound
        BossAudio.priority = 60;     //giving priority to hit sound to immediately playing the sound
        BossAudio.pitch = 1.4f;       //changing the pitch of sound
        BossAudio.Play();             //playing the sound
        SpawnID = Random.Range(1, 11);    //to set a random id for spawn place
        StartCoroutine(Spins());        //pause the script for sometime
    }

    public void BlobSpawning()
    {
        Instantiate(Blob, BlobSpawn.position, BlobSpawn.rotation);     //to start the blob attacking animation
        BossAudio.clip = SpawnSound;       //storing sound
        BossAudio.priority = 128;          //reduce priority to spawn sound 
        BossAudio.pitch = 0.6f;            //changing pitch of sound
        BossAudio.Play();                  //playing the sound of boss
    }

    void Respawn()
    {
      //  CanHitBoss = true;   
       
        if(SpawnID == 1)
        {
            Instantiate(BossVampire, SpawnPlace1.transform.position, SpawnPlace1.transform.rotation);    //for placing the boss at spwan place
        }
        if (SpawnID == 2)
        {
            Instantiate(BossVampire, SpawnPlace2.transform.position, SpawnPlace2.transform.rotation);    //for placing the boss at spwan place
        }
        if (SpawnID == 3)
        {
            Instantiate(BossVampire, SpawnPlace3.transform.position, SpawnPlace3.transform.rotation);    //for placing the boss at spwan place
        }
        if (SpawnID == 4)
        {
            Instantiate(BossVampire, SpawnPlace4.transform.position, SpawnPlace4.transform.rotation);    //for placing the boss at spwan place
        }
        if (SpawnID == 5)
        {
            Instantiate(BossVampire, SpawnPlace5.transform.position, SpawnPlace5.transform.rotation);    //for placing the boss at spwan place
        }
        if (SpawnID == 6)
        {
            Instantiate(BossVampire, SpawnPlace6.transform.position, SpawnPlace6.transform.rotation);    //for placing the boss at spwan place
        }
        if (SpawnID == 7)
        {
            Instantiate(BossVampire, SpawnPlace7.transform.position, SpawnPlace7.transform.rotation);    //for placing the boss at spwan place
        }
        if (SpawnID == 8)
        {
            Instantiate(BossVampire, SpawnPlace8.transform.position, SpawnPlace8.transform.rotation);    //for placing the boss at spwan place
        }
        if (SpawnID == 9)
        {
            Instantiate(BossVampire, SpawnPlace9.transform.position, SpawnPlace9.transform.rotation);    //for placing the boss at spwan place
        }
        if (SpawnID == 10)
        {
            Instantiate(BossVampire, SpawnPlace10.transform.position, SpawnPlace10.transform.rotation);    //for placing the boss at spwan place
        }
    }

    IEnumerator Spins()
    {
        yield return new WaitForSeconds(DestroyTime);     //pause the script for sometime
      //  if (SaveScript.WeaponID == 4)          //for the flame type gun
       // {
        //    SaveScript.BossHealth -= 0.1f;    //decrease the health of boss in case of flames
      //  }

        Respawn();              //calling Respawn() function
        Destroy(gameObject);    //destroy the current boss gameobject to create a new one on the different spawn place


    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(AttackTime);     //pause the script for sometime

        Anim.SetTrigger("Bite");      //starts again the boss attack animation after 3 seconds
        Attack = false;              //to start again the boss attack animation
    }
}
