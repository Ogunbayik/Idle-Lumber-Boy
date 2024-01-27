using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flatbed : MonoBehaviour
{
    private const string MOVEMENT_POINTS = "MovementPoints";

    private Building building;

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
    private int movementIndex;

    private int minLogCount = 3;
    private int maxLogCount = 7;
    private int desiredLogCount;
    
    private int minimumSpeed = 3;
    private int maximumSpeed = 6;
    private float movementSpeed;

    private float loadTimer;
    private float startLoadTimer = 1f;

    private bool isLoaded;
    void Start()
    {
        movementPoints = GameObject.Find(MOVEMENT_POINTS).transform.GetComponentsInChildren<Transform>();

        ActivateCarryItems(false);

        movementIndex = 1;
        movementSpeed = Random.Range(minimumSpeed, maximumSpeed);

        desiredPoint = GetNextPosition(movementIndex);
        desiredLogCount = Random.Range(minLogCount, maxLogCount);

        loadTimer = startLoadTimer;
        isLoaded = false;

        currentState = States.Move;
    }

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

                if (distanceBetweenDesired <= 0.1f && isLoaded)
                    Destroy(this.gameObject);

                break;
            case States.Load:

                if (building.canSell)
                    loadTimer -= Time.deltaTime;
                else
                    loadTimer = startLoadTimer;

                if (loadTimer <= 0f)
                {
                    loadTimer = startLoadTimer;
                    ActivateCarryItems(true);

                    if (desiredLogCount <= 0)
                    {
                        isLoaded = true;
                        if (isLoaded)
                        {
                            movementIndex++;
                            desiredPoint = GetNextPosition(movementIndex);
                            currentState = States.Move;
                        }
                    }
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

    public void DecreaseDesiredLogCount()
    {
        desiredLogCount--;
    }
    private void OnTriggerEnter(Collider other)
    {
        building = other.gameObject.GetComponent<SellPlace>().GetComponentInParent<Building>();
    }

    private void OnTriggerExit(Collider other)
    {
        building = null;
    }
}
