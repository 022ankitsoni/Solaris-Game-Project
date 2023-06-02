using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] GameObject Boss;     //for storing boss to activate the boss when player enters the room
    public bool isPlayerin = false;                                    // [SerializeField] GameObject BossHB;   //for activating the healthbar
    public GameObject PasswordPanel;  // [SerializeField] GameObject FinalMinions;   //for final minion wave
    public GameObject jee;
    public GameObject CorrectPassword;
    public GameObject WrongPassword;
    public GameObject DoorLock;
    public InputField inputPassword;
    public GameObject map2info;
    public GameObject map3info;
    public int ran;
    public string str = "",pass="";
    public bool newPass = false,closepanel=false;
    void OnTriggerEnter(Collider other)
    {
        //private bool isPlayerin = false;
        if (other.gameObject.CompareTag("Player"))
        {
            Boss.gameObject.SetActive(true);     //to activate the boss
            isPlayerin = true;
            // BossHB.gameObject.SetActive(true);   //to activate the healthBar
            //    FinalMinions.gameObject.SetActive(true);    //to activate the final wave of minion or to stop the new wave of minions
        }
        else 
        {
            Boss.gameObject.SetActive(false);
            isPlayerin = false;
        }

    }
    private void Start()
    {
        // Boss.gameObject.SetActive(false);
       
    }

    private void Update()
    {
        /*if (SaveScript.PlayerDead == true)
        {
            BossHB.gameObject.SetActive(false);   //to deactivate the healthBar
        }*/
        if (SaveScript.secondmapkey==true&& isPlayerin == true && Input.GetKeyDown(KeyCode.T))
        {
            Cursor.visible=true;
            SaveScript.isMap2 = true;
            //SceneManager.LoadScene(2);
            map2info.gameObject.SetActive(true);
        }
        if (SaveScript.thirdmapkey==true&& SaveScript.deathweapon==true&& isPlayerin == true && Input.GetKeyDown(KeyCode.B))
        {
            Cursor.visible = true;
            SaveScript.isMap2 = true;
            // SceneManager.LoadScene(3);
            map3info.gameObject.SetActive(true);
        }


        if (closepanel == false)
        {
            if (isPlayerin == true && Input.GetKeyDown(KeyCode.K))
            {
                SaveScript.PlayerDead = true;
                Cursor.visible = true;
                Boss.gameObject.SetActive(false);
                if (newPass == false)
                {
                    newPass = true;
                    for (int i = 0; i < 4; i++)
                    {
                        ran = Random.Range(1, 27);
                        str = str + (char)(97 + (ran % 26));
                    }
                    Debug.Log("str:" + str);
                    ran = Random.Range(1, 10);
                    Debug.Log("key:" + ran);
                    for (int i = 0; i < 4; i++)
                    {
                        pass = pass + (char)((((str[i] + ran) - 'a') % 26) + 97);
                    }
                    Debug.Log("pass:" + pass);
                    jee.GetComponent<PasswordKeeper>().Password.text = pass;
                }
                PasswordPanel.gameObject.SetActive(true);
                
            }
        }
        
    }

    public void map2()
    {
        SceneManager.LoadScene(2);
    }

    public void map3()
    {
        SceneManager.LoadScene(3);
    }

    public void close()
    {
        PasswordPanel.gameObject.SetActive(false);
        Cursor.visible = false;
        SaveScript.PlayerDead = false;
    }
    public void submit()
    {
        //CorrectPassword.gameObject.SetActive(false);
       // WrongPassword.gameObject.SetActive(true);
        if(inputPassword.text==str)
        {
            Debug.Log("door open");
            WrongPassword.gameObject.SetActive(false);
            CorrectPassword.gameObject.SetActive(true);
            DoorLock.gameObject.SetActive(false);
            closepanel = true;
        }
        else
        {
            Debug.Log("wrong password");
            CorrectPassword.gameObject.SetActive(false);
            WrongPassword.gameObject.SetActive(true);
        }
    }
}
