using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float moveSpeed = 10;
    public float jumpHeight = 3;

    Vector3 velocity;
    public float gravity = -9.81f;
    public bool gravity0;

    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask whatIsGround;
    bool isGrounded = false;

    public float dashTime;
    public float dashCooldown;
    private float originalMoveSpeed;
    private bool canDash = true;
    public float dashDisplacement;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        originalMoveSpeed = moveSpeed;
    }
    private void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(DoDash());
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        if (gravity0)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        

        controller.Move(velocity * Time.deltaTime);
    }
    public IEnumerator DoDash()
    {
        canDash = false;
        AudioManager.instance.Play("DashSFX");
        CameraShakeManager.instance.ShakeDamageCamera();
        moveSpeed *= dashDisplacement;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = originalMoveSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    public void DoJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
}
