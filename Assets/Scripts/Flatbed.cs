using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flatbed : MonoBehaviour
{
    private const string MOVEMENT_POINTS = "MovementPoints";

    private enum States
    {
        Load,
        Move
    }

    private States currentState;

    [SerializeField] private GameObject cargo;
    [SerializeField] private GameObject[] straps;

    private Transform[] movementPoints;
    private Transform desiredPoint;
    private float movementSpeed;
    private int movementIndex;
    private int lastMovementIndex;

    private float waitTimer;
    private float startWaitTimer = 5f;
    void Start()
    {
        movementPoints = GameObject.Find(MOVEMENT_POINTS).transform.GetComponentsInChildren<Transform>();

        ActivateCarryItems(false);
        currentState = States.Move;

        movementIndex = 1;
        desiredPoint = GetNextPosition(movementIndex);
        movementSpeed = Random.Range(3, 6);
        waitTimer = startWaitTimer;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case States.Move:
                var distanceBetweenDesired = Vector3.Distance(transform.position, desiredPoint.position);

                if(distanceBetweenDesired <= 0.1f)
                {
                    currentState = States.Load;
                }
                else
                {
                    HandleMovement(desiredPoint);
                }
                break;
            case States.Load:
                waitTimer -= Time.deltaTime;
                if(waitTimer <= 0f)
                {
                    waitTimer = startWaitTimer;

                    desiredPoint = GetNextPosition(movementIndex);
                    currentState = States.Move;
                    ActivateCarryItems(true);
                }
                break;
        }
    }

    private void HandleMovement(Transform point)
    {
        transform.position = Vector3.MoveTowards(transform.position, point.position, movementSpeed * Time.deltaTime);
    }

    private Transform GetNextPosition(int moveIndex)
    {
        return movementPoints[moveIndex];
    }

    private void ActivateCarryItems(bool isActive)
    {
        cargo.SetActive(isActive);
        for (int i = 0; i < straps.Length; i++)
        {
            straps[i].SetActive(isActive);

        }
    }
}
