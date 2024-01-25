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
        animator.SetBool(CUT_ANIMATION_HASH, true);
    }

    private void CutFinish()
    {
        animator.SetBool(CUT_ANIMATION_HASH, false);
    }

}
