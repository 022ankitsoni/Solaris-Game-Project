using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    //for UI interface

public class UIScript : MonoBehaviour
{
    [SerializeField] Text WeaponType;    //for changing weapontype on canvas panel
    [SerializeField] Text Ammo;          //for changing ammo
    [SerializeField] Text AmmoLabel;     //for changing ammo heading
    [SerializeField] Text HealthAmt;     //for changing health amount on canvas panel 
    [SerializeField] Text ScoreAmt;      //for changing the score 
    [SerializeField] Image BossHealthBar;   //for changing health bar of boss

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponType.text = SaveScript.WeaponName;    //accessing weapon name
        HealthAmt.text = SaveScript.HealthAmt.ToString();   //accessing the health amount
        ScoreAmt.text = SaveScript.Score.ToString("n0");    //accessing the score amount

        BossHealthBar.fillAmount = SaveScript.BossHealth;    //healthbar of boss controlled by bossHealth

        if (SaveScript.WeaponID == 1)
        {
            Ammo.text = SaveScript.AmmoAmt.ToString();  //accessing ammo amount of singleshot gun
        }
        if(SaveScript.WeaponID > 1)
        {
            Ammo.text = SaveScript.PickupAmmo.ToString(); //accessing ammo amount of other guns other than singleshot gun
        }

        if(SaveScript.WeaponID == 4)
        {
            AmmoLabel.text = "Fuel";
            Ammo.text = (Mathf.Round(SaveScript.PickupAmmo).ToString());   //for rounding off the fuel amount for not showing digits after decimal point    
        }
        else
        {
            AmmoLabel.text = "Ammo";
        }
    }
}
