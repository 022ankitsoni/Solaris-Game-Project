using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
   public void BackToMenu()
    {
        SceneManager.LoadScene(0);        //back to home page
    }
}
