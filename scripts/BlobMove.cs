using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMove : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 2.0f;     //to store the moving speed of blob which is the fire coming out from mouth of boss
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * MoveSpeed;       //current position of blob is updated in forward direction
    }

    private void OnTriggerEnter(Collider other)
    {
        if(SaveScript.PlayerDead == false)
        {
            if (other.gameObject.CompareTag("Stone"))
            {
                Destroy(gameObject);
            }
            if (other.gameObject.CompareTag("Player"))
            {
                SaveScript.HealthAmt -= 5;      //decreases the health
                other.transform.gameObject.SendMessage("GetHit");   //sends message to the player using GetHit function present in playermove script
                Destroy(gameObject);
            }
        }
    }
}
