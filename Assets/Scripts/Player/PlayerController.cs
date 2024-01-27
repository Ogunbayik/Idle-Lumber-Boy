using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";
    private const string CUT_ANIMATION_HASH = "Cut";

    private CharacterController controller;
    private Animator animator;

    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform body;

    private float gravity = 0.5f;
    private float movementY;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 movementDirection;

    private bool isMove;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        HandleMovement();

        if (Input.GetKey(KeyCode.Space) && !isMove)
            CutStart();
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(VERTICAL_INPUT);

        if (controller.isGrounded)
            movementY = 0f;
        else
            movementY -= gravity * Time.deltaTime;

        movementDirection = new Vector3(horizontalInput, movementY, verticalInput);
        movementDirection.Normalize();

        if (movementDirection != Vector3.zero)
        {
            controller.Move(movementDirection * movementSpeed * Time.deltaTime);
            isMove = true;
        }
        else
            isMove = false;

        HandleRotation();
    }

    private void HandleRotation()
    {
        if (movementDirection != Vector3.zero)
        {
            var rotationY = Quaternion.LookRotation(movementDirection, Vector3.up);
            body.transform.rotation = Quaternion.RotateTowards(body.transform.rotation, rotationY, 270 * Time.deltaTime);
        }
    }

    private void CutStart()
    {
        animator.SetBool(CUT_ANIMATION_HASH, true);
    }

    private void CutFinish()
    {
        animator.SetBool(CUT_ANIMATION_HASH, false);
    }

}
