using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Health;
    public string Weapon;
    public int Weaponid;
    public float Ammo;
    public int Score;
    public  float pickupAmmo;
   // public bool ismap2 = false;;

    public PlayerData ()
    {
        Health = SaveScript.HealthAmt;
        Weapon = SaveScript.WeaponName;
        Weaponid = SaveScript.WeaponID;
        Ammo = SaveScript.AmmoAmt;
        Score = SaveScript.Score;
        pickupAmmo = SaveScript.PickupAmmo;

       // ismap2 = SaveScript.isMap2;
    }
}
