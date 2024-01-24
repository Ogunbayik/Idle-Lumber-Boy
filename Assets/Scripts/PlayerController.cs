using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";

    [SerializeField] private float movementSpeed;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 movementDirection;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        HandleMovement();

        if (Input.GetKey(KeyCode.Space))
            CutStart();
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(VERTICAL_INPUT);

        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);
        movementDirection.Normalize();
        controller.Move(movementDirection * movementSpeed * Time.deltaTime);
    }

    private void CutStart()
    {
        animator.SetBool("Cut", true);
    }

    private void CutFinish()
    {
        animator.SetBool("Cut", false);
    }

}
