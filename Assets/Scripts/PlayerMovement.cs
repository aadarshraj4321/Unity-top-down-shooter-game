using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController characterController;
    private PlayerControls controls;
    private Animator animator;
    private Player player;




    [Header("Movement Data")]
    [SerializeField] private float playerWalkingSpeed;
    [SerializeField] private float gravityScale = 9.81f;
    private float verticalVelocity;
    private Vector3 movementDirection;



    [Header("Running Data")]
    [SerializeField] private float runningSpeed;
    private bool isRunning;
    private float speed;



    [Header("Aim Data")]
    [SerializeField] private Transform aimObject;
    [SerializeField] private LayerMask aimLayerMask;
    private Vector3 aimLookingDirection;




    private Vector2 moveInput;
    private Vector2 aimInput;







    private void Start()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        speed = playerWalkingSpeed;

        assignInput();
    }



    private void Update()
    {
        applyMovement();
        applyAimTowardsMouse();
        AnimatorControllers();
    }



    private void AnimatorControllers()
    {
        float xVelocity = Vector3.Dot(movementDirection.normalized, transform.right);
        float zVelocity = Vector3.Dot(movementDirection.normalized, transform.forward);

        animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);

        bool isRunningAnimation = isRunning && movementDirection.magnitude > 0;
        animator.SetBool("isRunning", isRunningAnimation);
    }




    private void applyMovement()
    {
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
        applyGravity();

        if (movementDirection.magnitude > 0)
        {
            characterController.Move(movementDirection * Time.deltaTime * speed);
        }
    }




    private void applyAimTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(aimInput);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
        {
            aimLookingDirection = hitInfo.point - transform.position;
            aimLookingDirection.y = 0f;
            aimLookingDirection.Normalize();

            transform.forward = aimLookingDirection;
            aimObject.position = new Vector3(hitInfo.point.x, transform.position.y + 1, hitInfo.point.z);

        }

    }



    private void applyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity -= gravityScale * Time.deltaTime;
            movementDirection.y = verticalVelocity;
        }
        else
        {
            verticalVelocity = -.5f;
        }
    }









    private void assignInput()
    {

        controls = player.controls;
        //controls.Character.Fire.performed += context => shoot();

        controls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        controls.Character.Movement.canceled += context => moveInput = Vector2.zero;

        controls.Character.Running.performed += context =>
        {
            speed = runningSpeed;
            isRunning = true;
        };
        controls.Character.Running.canceled += context =>
        {
            speed = playerWalkingSpeed;
            isRunning = false;
        };


        controls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        controls.Character.Aim.canceled += context => aimInput = Vector2.zero;

    }


}
