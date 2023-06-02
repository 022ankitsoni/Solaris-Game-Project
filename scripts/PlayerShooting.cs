using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Transform MuzzleSpawn;   //variable for muzzlespawn of gun
    [SerializeField] GameObject MuzzleFlash;  //variable for muzzleFlash of gun
    [SerializeField] GameObject ImpactStone;  //variable for impact when bullet hits any object
    [SerializeField] GameObject ImpactMetal;  //variable for impact when bullet hits any metal
    [SerializeField] AudioClip SingleShotSound;  //variable for storing audio clip of single shot sound
    [SerializeField] AudioClip RapidShotSound;   //variable for storing audio clip of rapid shot sound
    [SerializeField] float RapidDelay = 0.1f;    //variable for delaying rapidshotsond of gun
    [SerializeField] GameObject GrenadeSmoke;   //variable for grenade launch
    [SerializeField] AudioClip GrenadeSound;    //variable for storing audio clip of grenade sound
    [SerializeField] GameObject GrenadeExplosion;  //variable for explosion when grenade hits any object
    [SerializeField] GameObject Flames;      //for gun of flames
    [SerializeField] GameObject winpanel;
    [SerializeField] AudioClip FlameSound;    //variable for storing audio clip of flamesound
    [SerializeField] AudioClip PickupFX;   //variable for storing audio clip of pickup sound
    [SerializeField] GameObject BloodImpact;    //variable for blood impact of minion
    [SerializeField] float ImpactDistance = 0.001f; //for storing distance for blood impact on minion

    private bool RapidPlay = true;     //for checking and starting the rapid play sound
    private bool RapidShooting = true;    //for rapidshot delay
    private bool FireFuel = false;        //for changing fuel of flame thrower
    private bool CanHitBoss = true;      //to stop again and again hitting of boss before its vanish
    [SerializeField] LayerMask PlayerLayer;     //to shoot the minion when it is close to player

    private AudioSource PlayerAudio;      //variable for storing audio component or audio clip

    RaycastHit hit;      //variable for casting straight ray for bullet movement and store point where bullet its
    // Start is called before the first frame update
    void Start()
    {
        PlayerAudio = GetComponent<AudioSource>();   //storing audio component
        Flames.gameObject.SetActive(false);    //in beginning there are no flames
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.PlayerDead == false)
        {
            if (SaveScript.WeaponID == 1)     //checking if weapon type is 1
            {
                if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0) && SaveScript.AmmoAmt > 0)   //check if rightmouse button is on hold and leftmouse button is pressed
                {
                    Instantiate(MuzzleFlash, MuzzleSpawn.position, MuzzleSpawn.rotation); //initiate MuzzleFlash vaiable to show flash

                    SaveScript.AmmoAmt -= 1;     //for decreasing ammo amount when firing the gun

                    PlayerAudio.clip = SingleShotSound;  //load clip to PlayerAudio variable for playing clip
                    PlayerAudio.loop = false;    //for playing in loop
                    PlayerAudio.pitch = 1;
                    PlayerAudio.Play();

                    Hits();     //calling Hits() function
                }
            }
            if (SaveScript.WeaponID == 2)     //checking if weapon type is 2
            {
                if (Input.GetMouseButton(1) && Input.GetMouseButton(0))   //check if rightmouse button is on hold and leftmouse button is pressed
                {
                    Instantiate(MuzzleFlash, MuzzleSpawn.position, MuzzleSpawn.rotation); //initiate MuzzleFlash vaiable to show flash

                    if (RapidPlay == true)     //for not loading the audio clip multiple times in a second
                    {
                        RapidPlay = false;   //prevents to load multiple times
                        PlayerAudio.clip = RapidShotSound;  //load clip to PlayerAudio variable for playing clip
                        PlayerAudio.loop = true;    //for playing in loop
                        PlayerAudio.pitch = 3;
                        PlayerAudio.Play();
                    }
                    if(SaveScript.BossHealth <= 0)
                    {
                        PlayerAudio.Stop();
                        winpanel.gameObject.SetActive(true);
                    }
                    if (RapidShooting == true)
                    {
                        RapidShooting = false;
                        StartCoroutine(RapidFire());   //to pause the script for some time 
                    }
                }
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || SaveScript.PickupAmmo <= 0)  //when releasing left mouse button
                {
                    PlayerAudio.Stop();
                    RapidPlay = true;
                }
            }

            if (SaveScript.WeaponID == 3)     //checking if weapon type is 3
            {
                if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))   //check if rightmouse button is on hold and leftmouse button is pressed
                {
                    Instantiate(GrenadeSmoke, MuzzleSpawn.position, MuzzleSpawn.rotation); //initiate GrenadeSmoke vaiable to show flash

                    SaveScript.PickupAmmo -= 1;     //decreasing ammo amount when firing the grenade

                    PlayerAudio.clip = GrenadeSound;  //load clip to PlayerAudio variable for grenade sound clip
                    PlayerAudio.loop = false;    //for playing in loop
                    PlayerAudio.pitch = 1;
                    PlayerAudio.PlayDelayed(0.3f);

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);   //creating ray where mouse points
                    if (Physics.Raycast(ray, out hit, 1000, ~PlayerLayer))   //for checking if ray is casting within 1000 units
                    {
                        StartCoroutine(Grenade());   //for delaying the grenade explosion after the smoke ends
                    }
                }
            }


            if (SaveScript.WeaponID == 4)     //checking if weapon type is 4
            {
                if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))   //check if rightmouse button is on hold and leftmouse button is pressed
                {
                    Flames.gameObject.SetActive(true);      //flames start 

                    if (RapidPlay == true)     //for not loading the audio clip multiple times in a second
                    {
                        RapidPlay = false;   //prevents to load multiple times
                        FireFuel = true;      //start changing fire fuel    
                        PlayerAudio.clip = FlameSound;  //load clip to PlayerAudio variable for playing clip
                        PlayerAudio.loop = true;    //for playing in loop
                        PlayerAudio.pitch = 0.1f;
                        PlayerAudio.Play();
                    }
                }

                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                {
                    Flames.gameObject.SetActive(false);      //flames stop
                    if (RapidPlay == false)     //for stoping the audio clip
                    {
                        PlayerAudio.Stop();
                        FireFuel = false;       //stop changing fuel
                        RapidPlay = true;
                    }
                }
            }

            if (FireFuel == true)
            {
                SaveScript.PickupAmmo -= 3 * Time.deltaTime;    //decreasing fuel with seconds of time without changing 60 times per second
                if (SaveScript.PickupAmmo <= 0)             //for stoping the flame animation
                {
                    Flames.gameObject.SetActive(false);      //flames stop
                    if (RapidPlay == false)     //for stoping the audio clip
                    {
                        PlayerAudio.Stop();
                        FireFuel = false;       //stop changing fuel
                        RapidPlay = true;
                    }
                }
            }
        }
    }

    void Hits()
    {
       // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);   //creating ray where mouse points
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, 1000, ~PlayerLayer))   //for checking if ray is casting within 1000 units and PlayerLayer to exclude the player from raycast when minion is close to the player
        {
            if(hit.transform.tag == "Stone")    //if hitpoint is stone
            {
                Instantiate(ImpactStone, hit.point, Quaternion.LookRotation(hit.normal));   //to facing impact opposite to surface of wall
            }
            if (hit.transform.tag == "Metal")    //if hitpoint is metal
            {
                Instantiate(ImpactMetal, hit.point, Quaternion.LookRotation(hit.normal));   //to facing impact opposite to surface of wall
            }
            if (hit.transform.tag == "ExplodingBarrel")    //if hitpoint is barrel
            {
                hit.transform.gameObject.SendMessage("Explode");   //sends message to the barrel
            }
            if (hit.transform.tag == "Minion")    //if hitpoint is minion
            {
                Instantiate(BloodImpact, hit.point + hit.normal * ImpactDistance, Quaternion.LookRotation(hit.normal));   //for blood impact on minion
                hit.transform.gameObject.SendMessageUpwards("MinionDeath");  //sends message of minion death to the parent
            }
            if(hit.transform.tag == "Mutant")
            {
                Instantiate(BloodImpact, hit.point + hit.normal * ImpactDistance, Quaternion.LookRotation(hit.normal));   //for blood impact on minion
            }
            if(hit.transform.tag == "HeadQueen")
            {
                Instantiate(BloodImpact, hit.point + hit.normal * ImpactDistance, Quaternion.LookRotation(hit.normal));   //for blood impact on minion
                hit.transform.gameObject.SendMessageUpwards("QueenFreeze");  
                SaveScript.BossHealth -= 1;
                Debug.Log("boss health: "+SaveScript.BossHealth);
            }
            if (hit.transform.tag == "HeadMutant")
            {
                Instantiate(BloodImpact, hit.point + hit.normal * ImpactDistance, Quaternion.LookRotation(hit.normal));   //for blood impact on minion
                hit.transform.gameObject.SendMessageUpwards("MutantFreeze");
            }
            if (hit.transform.tag == "Boss")    //if hitpoint is Boss
            {
                if (CanHitBoss == true)
                {
                    CanHitBoss = false;
                    Instantiate(BloodImpact, hit.point + hit.normal * ImpactDistance, Quaternion.LookRotation(hit.normal));   //for blood impact on Boss
                    SaveScript.BossHealth -= 1;      //decrease the health of boss
                    SaveScript.Score += 1000;          //increasing the score
                    hit.transform.gameObject.SendMessage("Hit");  //sends message of Boss death
                    StartCoroutine(delay());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("RapidFireAmmo")) //checks the tag
        {
            SaveScript.WeaponID = 2;                //setting the weapon
            SaveScript.WeaponName = "Rapid Fire";   //changing weapon name for panel
            SaveScript.PickupAmmo = 100f;          //changing ammo amount for panel
            PickupSound();                          //calling the fucntion
            Destroy(other.gameObject);        //destroy the pickup object
        }
        if (other.gameObject.CompareTag("GrenadeAmmo")) //checks the tag
        {
            SaveScript.WeaponID = 3;            //setting the weapon
            SaveScript.WeaponName = "Grenade Launcher";   //changing weapon name for panel
            SaveScript.PickupAmmo = 3f;          //changing ammo amount for panel
            PickupSound();                      //calling the fucntion
            Destroy(other.gameObject);    //destroy the pickup object
        }
        if (other.gameObject.CompareTag("Flamethrower")) //checks the tag
        {
            SaveScript.WeaponID = 4;            //setting the weapon
            SaveScript.WeaponName = "Flame Thrower";   //changing weapon name for panel
            SaveScript.PickupAmmo = 100f;          //changing ammo amount for panel
            PickupSound();                      //calling the fucntion
            Destroy(other.gameObject);    //destroy the pickup object
        }
        if (other.gameObject.CompareTag("map3")) //checks the tag
        {
            SaveScript.thirdmapkey = true;
            PickupSound();                      //calling the fucntion
            Destroy(other.gameObject);    //destroy the pickup object
        }
        if (other.gameObject.CompareTag("map2")) //checks the tag
        {
            SaveScript.secondmapkey = true;
            PickupSound();                      //calling the fucntion
            Destroy(other.gameObject);    //destroy the pickup object
        }
        if (other.gameObject.CompareTag("HealthPickup")) //checks the tag
        {
            /* SaveScript.HealthAmt += 40;            //increasing the health amount
             if(SaveScript.HealthAmt >=100)         //if health amount becomes > 100 when player gets health pickup 
             {
                 SaveScript.HealthAmt = 100;
             }*/
            SaveScript.deathweapon = true;
            PickupSound();                      //calling the fucntion
            Destroy(other.gameObject);    //destroy the pickup object
        }
    }

    void PickupSound()
    {
        PlayerAudio.clip = PickupFX;
        PlayerAudio.loop = false;    //for not playing in loop
        PlayerAudio.pitch = 0.7f;
        PlayerAudio.Play();
    }
    IEnumerator RapidFire()
    {
        yield return new WaitForSeconds(RapidDelay);    //pause the script for 0.1 secondds

        SaveScript.PickupAmmo -= 1;     //decreasing ammo amount when firing the gun
        Hits();     //calling Hit function
        RapidShooting = true;
    }

    IEnumerator Grenade()
    {
        yield return new WaitForSeconds(0.3f);    //for delay 
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, 1000, ~PlayerLayer))
        {
            Instantiate(GrenadeExplosion, hit.point, Quaternion.LookRotation(hit.normal));   //initiate grenade explosion
        }
        if (hit.transform.tag == "ExplodingBarrel")    //if hitpoint is barrel
        {
            hit.transform.gameObject.SendMessage("Explode");   //sends message to the barrel
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.6f);
        CanHitBoss = true;
    }
}
