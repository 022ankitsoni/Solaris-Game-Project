using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawn : MonoBehaviour
{
    [SerializeField] GameObject RunningMinion;      //store running minion for producing new running minions from spawning place
    [SerializeField] GameObject CrawlingMinion;     //store running minion for producing new crawling minions from spawning place
    [SerializeField] GameObject DraggingMinion;     //store running minion for producing new dragging minions from spawinng place
    [SerializeField] Transform SpawnPlace;          //variable to store spawning place for producing new minions

    [SerializeField] GameObject MinionSpawnPlace1, MinionSpawnPlace2, MinionSpawnPlace3, MinionSpawnPlace4, MinionSpawnPlace5, MinionSpawnPlace6, MinionSpawnPlace7, MinionSpawnPlace8, MinionSpawnPlace9, MinionSpawnPlace10;        //for storing spawn places to change the position of minions
    [SerializeField] GameObject MinionSpawnPlace;     //to store the spawnplace for minion

    private int MinionSpawnID = 0;                 //to store a particular spawn place

    private bool CanSpawn = true;                   //to check if spawning place can produce minions or not
    // Start is called before the first frame update
    void Start()
    {
        //SaveScript.MinionCount = 0;
        StartCoroutine(Spawning());
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("update");
        if (SaveScript.MinionCount > 100)      //check if count of minions coming from spwaning place < 100 to stop further producing of minion
        {
            //if (CanSpawn == true)
            //{
                CanSpawn = false;
              //  StartCoroutine(Spawning());     //for pausng the script for sometime
           // }
        }
        else
        {
            // SaveScript.MinionCount = 0;
            // StartCoroutine(Spawning());
            CanSpawn = true;
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

       // MinionSpawnPlace.SetActive(true);
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        Debug.Log("spawn");
        Debug.Log("minioncount= " + SaveScript.MinionCount);
        yield return new WaitForSeconds(1f);    //pause the script for 0.1 second

        Instantiate(RunningMinion, MinionSpawnPlace.transform.position, SpawnPlace.transform.rotation);   //produce running minions
        SaveScript.MinionCount += 1;          //for counting minion coming from spawning place

        yield return new WaitForSeconds(1f);    //pause the script for 1.5 second

        Instantiate(RunningMinion, MinionSpawnPlace.transform.position, SpawnPlace.transform.rotation);   //produce running minions
        SaveScript.MinionCount += 1;          //for counting minion coming from spawning place

        yield return new WaitForSeconds(1f);    //pause the script for 2 second

        Instantiate(CrawlingMinion, MinionSpawnPlace.transform.position, SpawnPlace.transform.rotation);   //produce Crawling minions
        SaveScript.MinionCount += 1;          //for counting minion coming from spawning place

        yield return new WaitForSeconds(1f);    //pause the script for 0.8 second

        Instantiate(DraggingMinion, MinionSpawnPlace.transform.position, SpawnPlace.transform.rotation);   //produce Dragging minions
        SaveScript.MinionCount += 1;          //for counting minion coming from spawning place

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

       // CanSpawn = true;               //to start again this whole process of producing minion
        MinionSpawnID = Random.Range(1, 11);    //to set a random id for spawn place
        
        //MinionSpawnPlace.SetActive(false);
        if(CanSpawn==true)
        MinionRespawn();
        Destroy(gameObject);
    }
}
