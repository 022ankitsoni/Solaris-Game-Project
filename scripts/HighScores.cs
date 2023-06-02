using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public static int HighScore;               //for storing highscore
    [SerializeField] GameObject ScoreKeeper;   //to keep the highscore permanently when changing the scene(as changing scene destroys everything of previous scene)
   // private bool SetScore = false;             //to set the score
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(ScoreKeeper);       //not destroying highscore when changing the scene
        HighScore = PlayerPrefs.GetInt("NewHighScore");    //to retrieve the score from computer
    }

    // Update is called once per frame
    void Update()
    {
       /* if(SaveScript.WinScore == true)
        {
            SetScore = true;          //to set the highscore
        }

        if(SetScore == true)
        {
          //  PlayFabController.SetStats();
            SetScore = false;*/
            if(SaveScript.Score > HighScore)
            {
                PlayerPrefs.SetInt("NewHighScore", SaveScript.Score);     //update the highscore
                PlayerPrefs.Save();                 //to save the highscore in the computer system permanently
            }
        
    }
}
