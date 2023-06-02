using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public GroundCheck groundCheck;
    public float jumpForce = 30;
    public float gravity = -9.81f;
    public float gravityScale = 5;
    float velocity;
    public bool onGround = true;
    void Update()
    {
        velocity += gravity * gravityScale * Time.smoothDeltaTime;
        if (groundCheck.isGrounded && velocity <= 0)
        {
            velocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            onGround = false;
            velocity = jumpForce;
            transform.Translate(new Vector3(0, velocity, 0) * Time.smoothDeltaTime);
            StartCoroutine(JumpDelay());
        }
        
    }
    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.5f);
        onGround = true;
    }
}

