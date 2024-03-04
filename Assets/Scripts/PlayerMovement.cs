using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerControls controls;
    private Animator animator;




    [Header("Movement Data")]
    [SerializeField] private float playerWalkingSpeed;
    [SerializeField] private float gravityScale = 9.81f;
    private float verticalVelocity;
    private Vector3 movementDirection;



    [Header("Aim Data")]
    [SerializeField] private Transform aimObject;
    [SerializeField] private LayerMask aimLayerMask;
    private Vector3 aimLookingDirection;





    private Vector2 moveInput;
    private Vector2 aimInput;


    private void Awake()
    {
        controls = new PlayerControls();

        //controls.Character.Fire.performed += context => shoot();

        controls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        controls.Character.Movement.canceled += context => moveInput = Vector2.zero;

        controls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        controls.Character.Aim.canceled += context => aimInput = Vector2.zero;


    }


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }



    private void Update()
    {
        applyMovement();
        applyAimTowardsMouse();
        AnimatorControllers();
    }








    private void applyMovement()
    {
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);
        applyGravity();

        if (movementDirection.magnitude > 0)
        {
            characterController.Move(movementDirection * Time.deltaTime * playerWalkingSpeed);
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
            aimObject.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);

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



    private void AnimatorControllers()
    {
        float xVelocity = Vector3.Dot(movementDirection.normalized, transform.right);
        float zVelocity = Vector3.Dot(movementDirection.normalized, transform.forward);

        animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);
    }






    private void OnEnable()
    {
        controls.Enable();
    }


    private void OnDisable()
    {
        controls.Disable();
    }

}
