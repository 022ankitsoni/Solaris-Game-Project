using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
    //1 - singleshot, 2-rapidfire, 3-grenadelauncher, 4-flamethrower
    public static int WeaponID = 1;    //public static variable can be accessed by all the scipts and this vaiable will store which weapn is currently using
    public static string WeaponName;   //for displaying weapon name on canvas panel
    public static float PickupAmmo;    //for displaying other types of ammo other than singleshot
    public static float AmmoAmt;       //for displaying singleshot ammo
    public static int HealthAmt = 100;  //for displaying health of player
    public static int Score = 0;        //for displaying the score
    public static int MinionCount = 0;   //for storing minion count producing by spawnplace
    public static bool PlayerDead = false;   //to check if player is dead or not
    public static int BossHealth = 100;   //for storing the health of boss

    public static bool WinScore = false;      //for setting the score after win or die
    public static bool isMap2 = false;

    public static bool secondmapkey = false;
    public static bool thirdmapkey = false;
    public static bool deathweapon = false;


    [SerializeField] GameObject DeathPanel;     //to switch on death panel when player dies
    [SerializeField] GameObject EscapePanel;
    [SerializeField] GameObject WinPanel;       //to switch on win panel when player win
    [SerializeField] GameObject NextMapPanel;
    [SerializeField] GameObject Boss;

   // [SerializeField] GameObject InfoKeeper; 

    // Start is called before the first frame update
    void Start()
    {    if (isMap2 == false)
        {
            Cursor.visible = false;     //cursor is not visible in beginning
            AmmoAmt = 1000f;            //starting ammo for singleshot gun
            WeaponName = "SingleShot";  //starting weapon name will be singleshot
            HealthAmt = 100;            //reset the health
            PlayerDead = false;         //player is not dead
            BossHealth = 100;          //setting the boss health equal to 1
            WeaponID = 1;               //reset the wwapon id
            Score = 0;                  //reset the score
            MinionCount = 0;            //reset the minioncount
          //  DontDestroyOnLoad(InfoKeeper);
        }
        else
        {
            Cursor.visible = false;     //cursor is not visible in beginning
          //  AmmoAmt = 1000f;            //starting ammo for singleshot gun
           // WeaponName = "SingleShot";  //starting weapon name will be singleshot
           // HealthAmt = 100;            //reset the health
            PlayerDead = false;         //player is not dead
            BossHealth = 100;          //setting the boss health equal to 1
           // WeaponID = 1;               //reset the wwapon id
          //  Score = 0;                  //reset the score
            MinionCount = 0;            //reset the minioncount
        }
    }

    // Update is called once per frame
    void Update()
    {
       

        if (PickupAmmo <= 0)
        {
            WeaponID = 1;
            WeaponName = "SingleShot";
        }

        if(HealthAmt <= 0)
        {
            DeathPanel.gameObject.SetActive(true);     //activate the death panel when player dies
            PlayerDead = true;            //player is dead
            WinScore = true;               //to set the score after the player dies
            Cursor.visible = true;         //mouse cursor becomes visible
        }
        if(BossHealth<=0)
        {
            Cursor.visible = true;
            WinPanel.gameObject.SetActive(true);
        }

        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            EscapePanel.gameObject.SetActive(true);     //activate the death panel when player dies
            PlayerDead = true;            //player is dead
            WinScore = true;               //to set the score after the player dies
            Cursor.visible = true;         //mouse cursor becomes visible
        }

      /*  if (BossHealth <= 0.0f)
        {
            //  WinPanel.gameObject.SetActive(true);     //activate the death panel when player dies
            // PlayerDead = true;            //player is dead
            //  WinScore = true;              //to set the score after the boss dies
            //  Cursor.visible = true;         //mouse cursor becomes visible
            isMap2 = true;
            Destroy(Boss);
            PlayerDead = true;
            SaveSystem.SavePlayer();
            Cursor.visible = true;
            NextMapPanel.SetActive(true);
   
           // SceneManager.LoadScene(3);         //load new map

        }*/
    }

    public void Resume()
    {
        EscapePanel.gameObject.SetActive(false);     //activate the death panel when player dies
        PlayerDead = false;            //player is dead
      //  WinScore = true;               //to set the score after the player dies
        Cursor.visible = false;         //mouse cursor becomes visible
    }
    public void Replay()      //when clicking the replay button
    {
        if (isMap2 == true)
        {
            PlayerData data = SaveSystem.LoadPlayer();
            SaveScript.HealthAmt = data.Health;
            SaveScript.WeaponName = data.Weapon;
            SaveScript.WeaponID = data.Weaponid;
            Debug.Log(SaveScript.WeaponName);
            SaveScript.AmmoAmt = data.Ammo;
            SaveScript.PickupAmmo = data.pickupAmmo;
            SaveScript.Score = data.Score;
            SaveScript.isMap2 = true;

            SceneManager.LoadScene(3);

        }
        else
        {
            isMap2 = false;
            SceneManager.LoadScene(1);
        }
        
    }

    public void MenuReturn()      //when clicking the replay button
    {
        
        SceneManager.LoadScene(0);   //reload the whole scene
        isMap2 = false;
        Cursor.visible = true;
    }

    public void NextMap()
    {
        isMap2 = true;
        SceneManager.LoadScene(3);         //load new map
    }

  
}
