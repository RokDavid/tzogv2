using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region move
    Vector2 horizontalInput;
    [SerializeField] CharacterController controller;
    public float speed;
    public float sprintSpeed = 5f;
    public float normalSpeed = 5f;
    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }
    #endregion
    #region gravity && jump settings
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 5f;
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    bool jump;

    #endregion
    #region polish
    [SerializeField] WeaponSwing weaponSwing;
    // private bool isRunning = false;
    private Animator animator;
    Vector3 horizontalVelocity;
    #endregion
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        #region gravity
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (!isGrounded)
        {
            verticalVelocity.y += gravity * Time.deltaTime;
        }
        controller.Move(verticalVelocity * Time.deltaTime);

        #endregion
        #region horizontal move
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed * Time.deltaTime;
        controller.Move(horizontalVelocity);
        // AnimateRun();

        #endregion
        #region jump
        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = 0;
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            jump = false;
        }
        #endregion

    }

    // void AnimateRun()
    // {
    //     isRunning = (horizontalInput.x < 0 || horizontalInput.x > 0) || (horizontalInput.y < 0 || horizontalInput.y > 0) ? true : false;
    //     weaponSwing.ReceiveRunningBool(isRunning);
    //     // animator.SetBool("isRunning", isRunning);
    //     // Debug.Log("isrunning " + isRunning);
    //     // Debug.Log("horizontalInput.x " + horizontalInput.x);
    //     // Debug.Log("horizontalInput.y " + horizontalInput.y);

    // }

    public void OnJumpPressed()
    {
        jump = true;
    }
    // public void OnSprintPressed()
    // {
    //     sprint = true;
    // }
    // public void OnSprintReleased()
    // {
    //     sprint = false;
    // }

}
