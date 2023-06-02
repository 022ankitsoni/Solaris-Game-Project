using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;
using System.Text;
using CandyCoded.env;

[System.Serializable]

public class WebRequest : MonoBehaviour
{
    public InputField username, password;
    public Text fault, conn;
    public GameObject login;
    public GameObject playfab;
    public GameObject wrongusername;
    public string name;
    public string playername="";

    private bool flag = false;

    public static WebRequest instance;

    private void Start()
    {
        Cursor.visible = true;
    }

    private void Awake()
    {
        if (instance != null) // only allow one instance to exist in a scene
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Authentication

    public void Login()
    {
        if(username.text.Length==0)
        {
           // conn.gameObject.SetActive(false);
           // fault.gameObject.SetActive(true);
           // fault.text = "Empty Registration ID";
        }
        else
            StartCoroutine(Authorize());
    }
    

    private IEnumerator Authorize()
    {
       // fault.gameObject.SetActive(false);
       // conn.gameObject.SetActive(true);
        WWWForm form = new WWWForm();
        string uid = username.text;
        
        form.AddField("userID", uid);
        form.AddField("pass", password.text);

        if (env.TryParseEnvironmentVariable("APP_KEY", out string app_key))
            form.AddField("app_key", app_key);

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.version23.in/app/login", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // failure
                Debug.Log("connection not found");
            }
            else
            {
                // success
                
                string res = (www.downloadHandler.text);
                JObject obj = JObject.Parse(res);
                //Debug.Log(obj["data"]);
                
                if (obj["success"].ToString()=="false")
                {
                    // failure
                    Debug.Log("credential wrong");
                    wrongusername.gameObject.SetActive(true);
                }
                else
                {
                    // success
                    string str = obj["data"].ToString();
                    obj = JObject.Parse(str);
                    Debug.Log(obj["name"]);
                    Debug.Log(www.downloadHandler.text);
                    Debug.Log("login successful");
                    string temp = obj["name"].ToString();
                    for(int i=0;i<temp.Length;i++)
                    {
                        if ((temp[i]>='a'&&temp[i]<='z')||(temp[i]>='A'&&temp[i]<='Z')||(temp[i]>='0'&&temp[i]<='9'))
                            playername += temp[i];
                    }
                    Debug.Log("new name=" + playername);
                    //playfab.GetComponent<PlayFabController>().username = JsonUtility.FromJson<string>(res)+'('+username+')';
                    //Debug.Log(playfab.GetComponent<PlayFabController>().username); 
                     playfab.GetComponent<PlayFabController>().username =username.text.ToString();
                    playfab.GetComponent<PlayFabController>().userPassword = password.text.ToString();
                    playfab.GetComponent<PlayFabController>().OnClickLogin();
                    wrongusername.gameObject.SetActive(false);
                }
            }
        }
    }
}