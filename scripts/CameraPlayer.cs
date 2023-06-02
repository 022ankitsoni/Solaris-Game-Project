using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    private Animator Anim;     //variable for storing animator component
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();  //getting animator component
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.PlayerDead == false)
        {
            if (Input.GetMouseButtonDown(1))   //if pressing rightmouse button then set camera animation for aiming
            {
                Anim.SetBool("AimCam", true);
            }
            if (Input.GetMouseButtonUp(1))     //if releasing rightmouse button then unset camera aiming animation
            {
                Anim.SetBool("AimCam", false);
            }
        }
    }
}
