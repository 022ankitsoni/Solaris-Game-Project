using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //for AI navigation

public class AIMinion : MonoBehaviour
{
    [SerializeField] GameObject PlayerTarget;     //variable for storing player as target
    [SerializeField] float CrawlSpeed = 1.1f;     //variable for storing Crawl speed
    [SerializeField] float DragSpeed = 0.6f;      //variable for storing Drag speed
    [SerializeField] float RunSpeed = 2.3f;       //variable for speed of minion
    [SerializeField] float AttackDistance = 0.9f;  //variable for storing attack distance for minion
    [SerializeField] Collider MinionCol;        //variable for accessing the box collider of minion
  //  [SerializeField] GameObject MutantSound;
    private NavMeshAgent Nav;                     //variable for storing navigation agent component
    private Animator Anim;           //variable to talk with unity animator
    private float DistanceToPlayer;   //variable for storing distance between player and minion
    private bool CanMove = true;      //variable to check if minion can move or not
    private NavMeshObstacle NavObstacle;    //variable for Nav mesh agent to seperate the minions
    [Tooltip("1=Running,2=Crawl,3=Drag")]    //for showing suggestions when cursor is on miniontype option
    [SerializeField] int MinionType = 1;    //variable for storing type of minion among different types
    private float NavMinionSpeed;           //for storing speed of different types of minion
    private AnimatorStateInfo MinionInfo;    //to get state of minion running type
    private AnimatorStateInfo MinionInfo2;    //to get state of minion crawl type
    private AnimatorStateInfo MinionInfo3;    //to get state of minion drag type
    private bool Moving = true;              //to stop the movement if minion is dead
    private bool AlreadyDead = false;        //to stop shooting on dead minion 
    private bool AlredyFreeze = false;
    private int CountHit;
    [SerializeField] float RotationSpeed = 2.0f;    //variable for storing rotation of minion so that they can face the player when they are close to player

    // Start is called before the first frame update
    void Start()
    {
        CountHit = 0;
        Nav = GetComponent<NavMeshAgent>();     //getting Navigation agent component from unity
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");    //finding target using tag(i.e. Player)
        Anim = GetComponent<Animator>();        //getting animator component from unity  
        NavObstacle = GetComponent<NavMeshObstacle>();   //getting NavMeshObstacle component from unity
        NavObstacle.enabled = false;            //in beginning it is false
          
        if(MinionType == 1)
        {
            NavMinionSpeed = RunSpeed;    //for running
        }
        if(MinionType == 2)
        {
            Anim.SetLayerWeight(1, 1);         //setting layer-1 which is crawl layer with weight 1
            NavMinionSpeed = CrawlSpeed;       //setting speed as crawlspeed
        }
        if (MinionType == 3)
        {
            Anim.SetLayerWeight(2, 1);         //setting layer-2 which is drag layer with weight 1
            NavMinionSpeed = DragSpeed;        //setting speed as Dragspeed
        }
    }

    // Update is called once per frame
    void Update()
    {
         if (SaveScript.PlayerDead == false)
        {
            if (MinionType == 1)
            {
                MinionInfo = Anim.GetCurrentAnimatorStateInfo(0);    //get the state in baselayer(running layer)
            }
            else if (MinionType == 2)
            {
                MinionInfo = Anim.GetCurrentAnimatorStateInfo(1);    //get the state in crawllayer
            }
            else if (MinionType == 3)
            {
                MinionInfo = Anim.GetCurrentAnimatorStateInfo(2);    //get the state in draglayer
            }

            if (MinionInfo.IsTag("Dead") || MinionInfo2.IsTag("Dead") || MinionInfo3.IsTag("Dead"))
            {
                Moving = false;
                Nav.enabled = false;       //to stop navigation agent
            }
            else
            {
                Moving = true;
            }

            if (Moving == true)
            {
                DistanceToPlayer = Vector3.Distance(PlayerTarget.transform.position, transform.position);  //storing distance between player position and minion position

                if (DistanceToPlayer < AttackDistance)     //starts attacking the player
                {
                    Anim.SetBool("Attack", true);
                    //MinionCol.enabled = true;     //enable box collider to stop the minion
                    //  Nav.enabled = false;          //stoping the navigation to find the player
                    //  NavObstacle.enabled = true;   //switch ON the NavObstacle
                    Nav.isStopped = true;         //stops the NavMesh agent
                    CanMove = false;             //can't move while attacking the player
                    Vector3 Pos = (PlayerTarget.transform.position - transform.position).normalized;   //to calculate distance between palyer and minion when nav agent is switched off for rotating the minion
                    Quaternion PosRotation = Quaternion.LookRotation(new Vector3(Pos.x, 0, Pos.z));    //rotate the minion about x and z axis
                    transform.rotation = Quaternion.Slerp(transform.rotation, PosRotation, Time.deltaTime * RotationSpeed);  //to rotate the minion slowly towards the player
                }
                else if (DistanceToPlayer > AttackDistance + 1)  //stops attacking and running to the player
                {
                    Anim.SetBool("Attack", false);
                    //MinionCol.enabled = false;      //stoping the box coliider to reach the player
                    Nav.isStopped = false;         //starts the NavMesh agent
                                                   //NavObstacle.enabled = false;    //swtich OFF the NavObstacle
                                                   //Nav.enabled = true;          //starting the navigation to find the player
                    CanMove = true;              //can move to reach the player
                }

                if (CanMove == true)
                {
                    Nav.speed = NavMinionSpeed;    //setting navigation speed
                    Nav.SetDestination(PlayerTarget.transform.position); //setting destination(which is position of the player) for minion
                }
            }
        }
         else if(SaveScript.PlayerDead == true)
        {
            Nav.isStopped = true;
        }
    }

    public void MinionDeath ()
    {
        CountHit = CountHit + 1;
        if (AlreadyDead == false)
        {
            if(CountHit == 1)
            {
                SaveScript.Score += 10;
            }
            else if(CountHit == 2)
            {
                SaveScript.Score += 30;
            }

            else if (CountHit >= 3)
            {
                Anim.SetTrigger("Death");      //set the death tag of minion
                Nav.enabled = false;          //stoping the navigation to find the player
                AlreadyDead = true;            //to stop shooting on dead minion
                SaveScript.Score += 60;      //to increase the score by 1000
            }
        }
     }

    public void QueenFreeze()
    {
        if(SaveScript.BossHealth<=0)
        {

            if (SaveScript.PlayerDead == false)
            {
                Anim.SetTrigger("Burned");      //set the burned tag of minion
                Nav.enabled = false;
                SaveScript.Score += 100000;
            }
            SaveScript.PlayerDead = true;
        }
    }

    public void MutantFreeze()
    {
        Debug.Log("freeze");
        if (AlredyFreeze == false)
        {
            Anim.SetBool("Freeze", true);
            // Anim.SetBool("Attack", false);
            // MutantSound.SetActive(false);
            Nav.enabled = false;
            AlredyFreeze = true;
            StartCoroutine(Freeze());

        }
    }

    public void MinionBurned()
    {
        if (AlreadyDead == false)
        {
            if (MinionType == 1)
            {
                Anim.SetTrigger("Burned");      //set the burned tag of minion
                Nav.enabled = false;          //stoping the navigation to find the player
                AlreadyDead = true;            //to stop shooting on dead minion
                SaveScript.Score += 100;      //to increase the score by 1000
            }
            else
            {
                Anim.SetTrigger("Death");      //set the death tag of minion
                Nav.enabled = false;          //stoping the navigation to find the player
                AlreadyDead = true;            //to stop shooting on dead minion
                SaveScript.Score += 100;      //to increase the score by 1000
            }
        }
    }
    public void MinionBarrelExplode()        //function for death of minion by barrel explosion
    {
        if (AlreadyDead == false)
        {
            if (MinionType == 1)
            {
                Anim.SetTrigger("Burned");      //set the burned tag of minion
                Nav.enabled = false;          //stoping the navigation to find the player
                AlreadyDead = true;            //to stop shooting on dead minion
                SaveScript.Score += 100;      //to increase the score by 1000
            }
            else
            {
                Anim.SetTrigger("Death");      //set the death tag of minion
                Nav.enabled = false;          //stoping the navigation to find the player
                AlreadyDead = true;            //to stop shooting on dead minion
                SaveScript.Score += 100;      //to increase the score by 1000
            }
        }
    }

    public void DestroyOnDeath()
    {
        StartCoroutine(WaitForDestroy());    //to pause the script for sometime
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(0.5f);   //pause the script for 1.5 seconds
        SaveScript.MinionCount -= 1;        //decreases the minioncount when it is dead so that we can produce more minions from spawning place
        Destroy(gameObject);             //destroy minion object permanently
    }

    IEnumerator Freeze()
    {
        yield return new WaitForSeconds(10.0f);
        Anim.SetBool("Freeze", false);
        Anim.SetBool("Roaring", true);
        StartCoroutine(Roar());
       
      //  Anim.SetBool("Attack", true);
        //   MutantSound.SetActive(true);
    }

    IEnumerator Roar()
    {
        yield return new WaitForSeconds(5.0f);
        Anim.SetBool("Roaring", false);
        AlredyFreeze = false;
        Nav.enabled = true;
    }
}
