using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public  PlayFabController playFabInstance;

    [SerializeField] GameObject ContactPanel;
    [SerializeField] GameObject SaveLoadPanel;
    [SerializeField] GameObject map1info;

    [SerializeField] Text HighScoreValue;     //to store the highscore
    //public static string highscorevalue;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        HighScoreValue.text = HighScores.HighScore.ToString();    //stores the highscore
        //playFabInstance.Start();
        playFabInstance.StartCloudUpdatePlayerStats();
        playFabInstance.SetStats();
        playFabInstance.loginPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HighScoreValue.text = HighScores.HighScore.ToString();    //stores the highscore
        Cursor.visible = true;
    }

    public void OnClickLeaderboard()                           //when clicking leaderboard button
    {
        playFabInstance.GetLeaderboarder();
    }

    public void OnClickCloseLeaderboard()
    {
        playFabInstance.CloseLeaderboardPanel();
    }
    public void Play()
    {
        SaveLoadPanel.SetActive(true);
     //   SceneManager.LoadScene(1);        //load main scene
    }

    public void Info()
    {
        SceneManager.LoadScene(2);        //load info scene
    }

    public void Quit()
    {
        Application.Quit();               //quit the game
    }

    public void OnClickContact()
    {
        ContactPanel.SetActive(true);
    }

    public void OnClickCloseContact()
    {
        ContactPanel.SetActive(false);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        SaveScript.HealthAmt = data.Health;
        SaveScript.WeaponName = data.Weapon;
        SaveScript.WeaponID = data.Weaponid;
        SaveScript.PickupAmmo = data.pickupAmmo;
        SaveScript.AmmoAmt = data.Ammo;
        SaveScript.Score = data.Score;
        SaveScript.isMap2 = true;
        SceneManager.LoadScene(3);
    }

    public void newGame()
    {
        map1info.gameObject.SetActive(true);
        //SceneManager.LoadScene(1);
    }
    public void map1()
    {
      //  map1info.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
