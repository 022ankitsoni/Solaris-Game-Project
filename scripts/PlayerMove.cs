using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator Anim;     //variable for storing movements like forward and backward
    [SerializeField] float StillRotateSpeed = 0.5f;    //variable for storing rotating speed of player when moving mouse 
    [SerializeField] float WalkRotateSpeed = 1.5f;      //variable for storing rotating speed
    [SerializeField] float RunRotateSpeed = 1.5f;       //variable for storing running speed
    [SerializeField] float AimRotateSpeed = 0.6f;       //variable for storing aiming speed
    [SerializeField] GameObject Crosshair;            //variable for linking crosshair with this script
    [SerializeField] GameObject BloodFX;             //variable for bloodfx of player
  //  [SerializeField] GameObject player;
    private float RotateSpeed;
    public float speed = 2.0f;
    private AnimatorStateInfo PlayerInfo;        //variable for storing animator state
    private AnimatorStateInfo PlayerInfoL2;        //variable for storing animator state of layer-2
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
  //  CharacterController characterController;
  //  public float gravity = -20f;
  //  Vector3 moveVelocity;
    //  public float jumpForce = 15;
   // public float gravity = -9.81f;
   // float velocity;
   // private bool onGround = true;
   // public Rigidbody player;
    //[SerializeField] Rigidbody player;
    // float velocity;

    // private bool rotatemove = false;

    /* Rigidbody rigidBody;
     [SerializeField] GameObject stepRayUpper;
     [SerializeField] GameObject stepRayLower;
     [SerializeField] float stepHeight = 0.3f;
     [SerializeField] float stepSmooth = 2f;

     private void Awake()
     {
         rigidBody = GetComponent<Rigidbody>();

         stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
     }

     private void FixedUpdate()
     {
         stepClimb();
     }

     void stepClimb()
     {
         RaycastHit hitLower;
         if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
         {
             RaycastHit hitUpper;
             if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
             {
                 rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
             }
         }

         RaycastHit hitLower45;
         if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
         {

             RaycastHit hitUpper45;
             if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
             {
                 rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
             }
         }

         RaycastHit hitLowerMinus45;
         if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.1f))
         {

             RaycastHit hitUpperMinus45;
             if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.2f))
             {
                 rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
             }
         }
     } */


    // Start is called before the first frame update
  /*  void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }*/
    void Start()
    {
      //  player = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();    //connect animator with script and storing it in Anim 
        Crosshair.gameObject.SetActive(false);   //in the beginning crosshair is not visible
        BloodFX.gameObject.SetActive(false);     //in the beginning bloodfx is not visible
    }
    // Update is called once per frame
    void Update()
    {
      /*  if(characterController.isGrounded)
        {
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                moveVelocity.y = jumpForce;
                characterController.Move(moveVelocity * Time.deltaTime);
            }
        }*/
        if (SaveScript.PlayerDead == false)
        {
            PlayerInfo = Anim.GetCurrentAnimatorStateInfo(0);   //getting current animator state and 0 shows current base layer by default it is 0
            PlayerInfoL2 = Anim.GetCurrentAnimatorStateInfo(1);   //getting current animator state of layer-2and 1 shows react layer

            float MoveDirection = Input.GetAxis("Vertical");    //getting input from keyboard when pressing up and down key
            float SideMoveDirection = Input.GetAxis("Horizontal");
            //  float RotateDirection = Input.GetAxis("Mouse X");   //getting input from mouse when moving the mouse

             yaw += speedH * Input.GetAxis("Mouse X");
             pitch -= speedV * Input.GetAxis("Mouse Y");

             pitch = Mathf.Clamp(pitch, -35f, 35f);

             transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


            if (PlayerInfo.IsTag("Still"))          //checking if player is in still position
            {
                RotateSpeed = StillRotateSpeed;
                Crosshair.gameObject.SetActive(false);

            }

            if (PlayerInfo.IsTag("Walk"))          //checking if player is in walk position
            {
                RotateSpeed = WalkRotateSpeed;
            }

            if (PlayerInfo.IsTag("RightWalk"))          //checking if player is in walk position
            {
                RotateSpeed = WalkRotateSpeed;
            }

            if (PlayerInfo.IsTag("LeftWalk"))          //checking if player is in walk position
            {
                RotateSpeed = WalkRotateSpeed;
            }
          

            if (PlayerInfo.IsTag("Run"))          //checking if player is in run position
            {
                RotateSpeed = RunRotateSpeed;
            }
            if (PlayerInfo.IsTag("Aiming"))          //checking if player is in aiming position
            {
                RotateSpeed = AimRotateSpeed;
                Crosshair.gameObject.SetActive(true);
            }

            if (PlayerInfoL2.IsTag("Hit"))
            {
                Anim.SetLayerWeight(1, 1);    //setting react layer weight to 1 (layer, layerweight)
                BloodFX.gameObject.SetActive(true);   //bloodfx starts
            }
            else if (PlayerInfoL2.IsTag("Idle"))
            {
                Anim.SetLayerWeight(1, 0);    //setting react layer weight to 0 (layer, layerweight) means deactivating the layer-2
                BloodFX.gameObject.SetActive(false); //deactivate bloodfx
            }
        /*    velocity += gravity * Time.deltaTime;
            if (Input.GetKeyDown("space") && onGround)
            {
                Debug.Log("jump");
                 velocity = jumpForce;
               // player.velocity = Vector3.zero;
               // player.AddForce(new Vector3(0, jumpForce, 0) , ForceMode.Impulse);
                onGround = false;
                
            }
            transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);*/



            if (MoveDirection > 0 )          // if value is >0 player will walk forward
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))    //if leftshift is pressed player will run
                {
                    Anim.SetBool("Running", true);
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))    //if leftshift is pressed player will run
                {
                    Anim.SetBool("Running", false);
                }
                else
                {
                    Anim.SetBool("Walk", true);
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(MoveDirection > 0)
                {
                    Anim.SetBool("Running", true);
                }
                else if(MoveDirection < 0)
                {
                    Anim.SetBool("WalkBack", true);
                }
                else
                {
                    Anim.SetBool("Running", false);
                }
            }
            if (SideMoveDirection > 0 )          // if value is >0 player will walk forward
            {
                Debug.Log("side");
                  if (Input.GetKeyDown(KeyCode.LeftShift))    //if leftshift is pressed player will run
                  {
                      Anim.SetBool("RightRun", true);
                  }
                  else if (Input.GetKeyUp(KeyCode.LeftShift))    //if leftshift is pressed player will run
                  {
                      Anim.SetBool("RightRun", false);
                  }
                  else
                  {
                      Anim.SetBool("RightWalk", true);
                  }
            }
            if (SideMoveDirection < 0)          // if value is >0 player will walk forward
            {
                Debug.Log("side");
                  if (Input.GetKeyDown(KeyCode.LeftShift))    //if leftshift is pressed player will run
                  {
                      Anim.SetBool("LeftRun", true);
                  }
                  else if (Input.GetKeyUp(KeyCode.LeftShift))    //if leftshift is pressed player will run
                  {
                      Anim.SetBool("LeftRun", false);
                  }
                  else
                  {
                      Anim.SetBool("LeftWalk", true);
                  }
            }
           /* if (RotateDirection > 0)         //if value is >0 player will rotate
            {
                this.transform.Rotate(Vector3.up * RotateSpeed);   //using vector3 for 3d motion 
            }*/
            if (MoveDirection == 0)      //if value is =0 means when key is not pressed from keyboard player will stop moving
            {
                Anim.SetBool("Walk", false);        //setting parameter Walk in animator tab as false
                Anim.SetBool("WalkBack", false);
                Anim.SetBool("Running", false);
            }

            if (SideMoveDirection == 0)      //if value is =0 means when key is not pressed from keyboard player will stop moving
            {
                Anim.SetBool("RightWalk", false);        //setting parameter Walk in animator tab as false
                 Anim.SetBool("LeftWalk", false);        //setting parameter Walk in animator tab as false
              //  rotatemove = false;
              //  Anim.SetBool("WalkBack", false);
                Anim.SetBool("LeftRun", false);
                Anim.SetBool("RightRun", false);
            }

            if (MoveDirection < 0)       //if value is <0 player will walk backward
            {
                Anim.SetBool("WalkBack", true);
            }
           /* if (RotateDirection < 0)    //if value is <0 player will rotate in reverse direction
            {
                this.transform.Rotate(Vector3.up * -RotateSpeed);      //rotate with -ve speed
            }*/

            if (Input.GetMouseButtonDown(1))   //if rightmouse button is pressed and hold
            {
                Anim.SetBool("Aim", true);
            }
            if (Input.GetMouseButtonUp(1))    //if rightmouse button is not pressed or released
            {
                Anim.SetBool("Aim", false);
            }
        }
    }

    public void GetHit()
    {
        //Anim.SetLayerWeight(1, 1);    //setting react layer weight to 1 (layer, layerweight)
        Anim.SetTrigger("React");     //setting react trigger active
    }

    
}
