using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
//using PlayFab.Json;
//using PlayFab.PfEditor.Json;
using JsonObject = PlayFab.Json.JsonObject;
using System.Collections.Generic;

public class PlayFabController :MonoBehaviour
{
    public static PlayFabController PFC;

    public string userEmail="";
    public string userPassword;
    public string username;
    public GameObject loginPanel;
    public GameObject addLoginPanel;
    public GameObject recoverButton;
    public GameObject wrongusername;
    public WebRequest webuser;
   // private bool loginCheck = false;

    private void OnEnable()
    {
        if(PlayFabController.PFC == null)
        {
            PlayFabController.PFC = this;
        }
        else
        {
            if(PlayFabController.PFC !=this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void Start()
    {
        //userEmail = username.ToString() + "@gmail.com";
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "96587"; // Please change this value to your own titleId from PlayFab Game Manager
        }
         PlayerPrefs.DeleteAll();
        //  var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };    //requesting for player login
        //  PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);     //submitting the login 
      //  if (loginCheck == false)
      //  {
          //  loginPanel.SetActive(true);
            if (PlayerPrefs.HasKey("EMAIL"))
            {
                 
                userEmail = PlayerPrefs.GetString("EMAIL");
                userPassword = PlayerPrefs.GetString("PASSWORD");
                //   username = PlayerPrefs.GetString("USERNAME");
                var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };   //for user login from nitt email
                PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);     //submitting the login
            }
       // Cursor.visible = true;
          /*  else
            {
            var register = new RegisterPlayFabUserRequest { Username = username, Email = userEmail, Password = userPassword };
            PlayFabClientAPI.RegisterPlayFabUser(register, OnRegisterSuccess, OnRegisterFailure);
        }*/
    }
            
       // }
    

    #region Login
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");    //if login successfully
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
      //  PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = username }, OnDisplayName, OnLoginMobileFailure);
      //  Debug.Log("display name added");
        //  PlayerPrefs.SetString("USERNAME", username);
        // loginCheck = true;
        loginPanel.SetActive(false);
        recoverButton.SetActive(false);
        GetStats();
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");    //if login successfully
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        // PlayerPrefs.SetString("USERNAME", username);

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = username }, OnDisplayName, OnLoginMobileFailure);
     //   Debug.Log("display name added");
        GetStats();
        loginPanel.SetActive(false);
    }

    void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + " is your display name");
    }

    void OnLoginMobileFailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    private void OnLoginFailure(PlayFabError error)
    {
        var registerRequest = new RegisterPlayFabUserRequest { Username = username, Email = userEmail, Password = userPassword };   //for new user
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);    //for new user registration
    }
   

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());    //for login failure
        wrongusername.SetActive(true);
    }

    public void GetuserEmail(string emailIn)
    {
        userEmail = emailIn;     //to get email input
    }

    public void GetUserPassword(string passwordIn)
    {
        userPassword = passwordIn;   //to get password input
    }

    public void GetUsername(string usernameIn)
    {
       // username = usernameIn;       //to get username
        if (IsDigitsOnly(usernameIn) == true)
        {
            wrongusername.SetActive(false);
            username = usernameIn;       //to get username
        }
        else
        {
            wrongusername.SetActive(true);
            //username = usernameIn;
        }
    }

    bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }

    public void OnClickLogin()
    {
        /*  if (IsDigitsOnly(username) == true)
          {
              wrongusername.SetActive(false);*/
        Debug.Log("username:" + username.ToString());
        userEmail = username.ToString() + "@gmail.com";
        username = webuser.playername ;
        Debug.Log("function call "+userEmail.ToString());

            //  username = usernameIn;       //to get username
            var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };   //for user login from nitt email
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);     //submitting the login
       // }
       /* else
        {
            wrongusername.SetActive(true);
            //username = usernameIn;
        }*/
       
    }

    public void OpenAddLogin()
    {
        addLoginPanel.SetActive(true);
    }

    public void OnClickAddLogin()
    {
        var addLoginRequest = new AddUsernamePasswordRequest { Email = userEmail, Password = userPassword, Username = username };   //for new user
        PlayFabClientAPI.AddUsernamePassword(addLoginRequest, OnAddLoginSuccess, OnRegisterFailure);    //for new user registration
    }

    private void OnAddLoginSuccess(AddUsernamePasswordResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");    //if login successfully
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        // PlayerPrefs.SetString("USERNAME", username);
        addLoginPanel.SetActive(false);
    }
    #endregion Login

    public int playerHighScore = HighScores.HighScore;      //for player stats(displaying on leaderboard

    private void Update()
    {
        playerHighScore = HighScores.HighScore;
    }

    #region PlayerStats

    public void SetStats()
    {
        
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
               
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate { StatisticName = "PlayerHighScore", Value = playerHighScore},
            }
        },
        result => { Debug.Log("User Statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); }
            );
    }

    void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
            );
    }

    void OnGetStats(GetPlayerStatisticsResult result)
    {
       // var statValue = result.Statistics;
       // HighScores.HighScore = statValue.value;
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch(eachStat.StatisticName)
            {
                case "PlayerHighScore":
                    playerHighScore = eachStat.Value;
                    HighScores.HighScore = eachStat.Value;
                    break;
            }
           
                
            
        }
    }

    public void StartCloudUpdatePlayerStats()
    {
        playerHighScore = HighScores.HighScore;
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats",
            FunctionParameter = new { highScore = playerHighScore },
            GeneratePlayStreamEvent = true,
        }, OnCloudUpdateStats, OnErrorShared);
    }

    private static void OnCloudUpdateStats(ExecuteCloudScriptResult result)
    {
       //  Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
       // Debug.Log(result.ToJson());
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue);
        Debug.Log((string)messageValue);
    }

    private static void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    #endregion PlayerStats

    public GameObject leaderboardPanel;
    public GameObject listingPrefab;
    public Transform listingContainer;

    #region Leaderboard
    public void GetLeaderboarder()
    {
        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "PlayerHighScore", MaxResultsCount = 30 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeadboard, OnErrorLeaderboard);
    }

    void OnGetLeadboard(GetLeaderboardResult result)
    {
        leaderboardPanel.SetActive(true);
       // Debug.Log(result.Leaderboard[0].StatValue);
        foreach(PlayerLeaderboardEntry player in result.Leaderboard)
        {
            GameObject tempListing = Instantiate(listingPrefab, listingContainer);
            LeaderboardListing LL = tempListing.GetComponent<LeaderboardListing>();
            LL.playerNameText.text = player.DisplayName;
            LL.playerScoreText.text = player.StatValue.ToString();
            Debug.Log(player.DisplayName + ": " + player.StatValue);
        }
    }

    public void CloseLeaderboardPanel()
    {
        leaderboardPanel.SetActive(false);
        for(int i = listingContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(listingContainer.GetChild(i).gameObject);
        }
    }

    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    #endregion Leaderboard
}