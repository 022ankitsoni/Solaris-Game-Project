using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAlienDirectionSpawn : MonoBehaviour
{
    private GameObject PlayerTarget;       //to store the main player
    [SerializeField] float RotationSpeed = 2.0f;   //for rotation of boss to face the player
    [SerializeField] GameObject SpawnPlace1, SpawnPlace2, SpawnPlace3, SpawnPlace4, SpawnPlace5, SpawnPlace6, SpawnPlace7, SpawnPlace8, SpawnPlace9, SpawnPlace10;        //for storing spawn places to change the position of boss
    private Animator Anim;                   //to get the animator component
    private int SpawnID = 0;                 //to store a particular spawn place
    [SerializeField] float DestroyTime = 3.0f;    //to destroy the boss temporarily to reappear at different spawn place
    [SerializeField] GameObject BossVampire;     //to store the vampire boss

    // [SerializeField] Transform BlobSpawn;      //for storing blob spawn for attacking the player by boss
    //  [SerializeField] GameObject Blob;          //for storing the blob which is the fire coming from the mouth of boss
    private bool Attack = false;               //to check if boss can attack the blob or not
                                               // public bool CanHitBoss = false;            //to stop again and again hitting of boss before its vanishing
    [SerializeField] float AttackTime = 3.0f;         //to ensure how quickly the boss can attack after the first attack(time duration between two attacks) 
    private AudioSource BossAudio;             //for sound of boss
                                               //[SerializeField] AudioClip SpawnSound;      //storing spawn sound
                                               // [SerializeField] AudioClip HitSound;        //storing hit sound when player hits the boss
    private bool canspawn = true;                                         //  public GameObject canhitboss;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerTarget = GameObject.FindGameObjectWithTag("MiniAlienDirectionTarget");   //find player to store 
        //Anim = GetComponent<Animator>();
       // BossAudio = GetComponent<AudioSource>();      //getting audio source component
    }

    // Update is called once per frame
    void Update()
    {

       // Debug.Log("update");
        if (canspawn == true)
        {
            Debug.Log("true");
            canspawn = false;
               //to set a random id for spawn place
            Respawn();              //calling Respawn() function
        }

    }

    /*  public void Hit()
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
    */
    void Respawn()
    {
        //  CanHitBoss = true;   

        if (SpawnID == 1)
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
        SpawnID = Random.Range(1, 11);
        Debug.Log("spawn");
        StartCoroutine(Spins());        //pause the script for sometime
    }

    IEnumerator Spins()
    {
        yield return new WaitForSeconds(DestroyTime);     //pause the script for sometime
                                                          //  if (SaveScript.WeaponID == 4)          //for the flame type gun
                                                          // {
                                                          //    SaveScript.BossHealth -= 0.1f;    //decrease the health of boss in case of flames
                                                          //  }

        // Respawn();              //calling Respawn() function
        Destroy(gameObject);    //destroy the current boss gameobject to create a new one on the different spawn place
        //canspawn = true;
        Respawn();
    }
}
  /*  IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(AttackTime);     //pause the script for sometime

        Anim.SetTrigger("Bite");      //starts again the boss attack animation after 3 seconds
        Attack = false;              //to start again the boss attack animation
    }
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAlienDirectionSpawn : MonoBehaviour
{
    [SerializeField] Transform SpawnPlace;          //variable to store spawning place for producing new minions

    [SerializeField] GameObject MinionSpawnPlace1, MinionSpawnPlace2, MinionSpawnPlace3, MinionSpawnPlace4, MinionSpawnPlace5, MinionSpawnPlace6, MinionSpawnPlace7, MinionSpawnPlace8, MinionSpawnPlace9, MinionSpawnPlace10;        //for storing spawn places to change the position of minions
    [SerializeField] GameObject MinionSpawnPlace;     //to store the spawnplace for minion

    private int MinionSpawnID = 0;                 //to store a particular spawn place

    private bool CanSpawn = true;                   //to check if spawning place can produce minions or not
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
            if (CanSpawn == true)
            {
                CanSpawn = false;
                StartCoroutine(Spawning());     //for pausng the script for sometime
            }
    }

    void MinionRespawn()
    {
        //  CanHitBoss = true;   

        if (MinionSpawnID == 1)
        {
            Instantiate(MinionSpawnPlace, MinionSpawnPlace1.transform.position, MinionSpawnPlace1.transform.rotation);    //for placing the boss at spwan place
        }
        if (MinionSpawnID == 2)
        {
            Instantiate(MinionSpawnPlace, MinionSpawnPlace2.transform.position, MinionSpawnPlace2.transform.rotation);    //for placing the boss at spwan place
        }
        if (MinionSpawnID == 3)
        {
            Instantiate(MinionSpawnPlace, MinionSpawnPlace3.transform.position, MinionSpawnPlace3.transform.rotation);    //for placing the boss at spwan place
        }
        if (MinionSpawnID == 4)
        {
            Instantiate(MinionSpawnPlace, MinionSpawnPlace4.transform.position, MinionSpawnPlace4.transform.rotation);    //for placing the boss at spwan place
        }
        if (MinionSpawnID == 5)
        {
            Instantiate(MinionSpawnPlace, MinionSpawnPlace5.transform.position, MinionSpawnPlace5.transform.rotation);    //for placing the boss at spwan place
        }
        if (MinionSpawnID == 6)
        {
            Instantiate(MinionSpawnPlace, MinionSpawnPlace6.transform.position, MinionSpawnPlace6.transform.rotation);    //for placing the boss at spwan place
        }
        if (MinionSpawnID == 7)
        {
            Instantiate(MinionSpawnPlace, MinionSpawnPlace7.transform.position, MinionSpawnPlace7.transform.rotation);    //for placing the boss at spwan place
        }
        if (MinionSpawnID == 8)
        {
            Instantiate(MinionSpawnPlace, MinionSpawnPlace8.transform.position, MinionSpawnPlace8.transform.rotation);    //for placing the boss at spwan place
        }
        if (MinionSpawnID == 9)
        {
            Instantiate(MinionSpawnPlace, MinionSpawnPlace9.transform.position, MinionSpawnPlace9.transform.rotation);    //for placing the boss at spwan place
        }
        if (MinionSpawnID == 10)
        {
            Instantiate(MinionSpawnPlace, MinionSpawnPlace10.transform.position, MinionSpawnPlace10.transform.rotation);    //for placing the boss at spwan place
        }

         MinionSpawnPlace.SetActive(true);
         StartCoroutine(Spawning());
        Debug.Log("Spawn place changed");
    }

    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(3f);    //pause the script for 0.1 second

        /*  yield return new WaitForSeconds(2.5f);    //pause the script for 2.5 second

          Instantiate(RunningMinion, SpawnPlace.position, SpawnPlace.rotation);   //produce running minions
          SaveScript.MinionCount += 1;          //for counting minion coming from spawning place

          yield return new WaitForSeconds(2.2f);    //pause the script for 2.2 second

          Instantiate(CrawlingMinion, SpawnPlace.position, SpawnPlace.rotation);   //produce Crawling minions
          SaveScript.MinionCount += 1;          //for counting minion coming from spawning place

          yield return new WaitForSeconds(3.6f);    //pause the script for 3.6 second

          Instantiate(RunningMinion, SpawnPlace.position, SpawnPlace.rotation);   //produce running minions
          SaveScript.MinionCount += 1;          //for counting minion coming from spawning place

          yield return new WaitForSeconds(1f);    //pause the script for 1 second

          Instantiate(DraggingMinion, SpawnPlace.position, SpawnPlace.rotation);   //produce Dragging minions
          SaveScript.MinionCount += 1;          //for counting minion coming from spawning place 

          yield return new WaitForSeconds(0.5f);    //pause the script for 0.5 second*/

     /*   CanSpawn = true;
        MinionSpawnID = Random.Range(1, 11);    //to set a random id for spawn place

     //MinionSpawnPlace.SetActive(false);
     MinionRespawn();
     Destroy(gameObject);
                //to start again this whole process of producing minion
    }
}*/
