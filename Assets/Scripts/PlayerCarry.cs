using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCarry : MonoBehaviour
{
    private event EventHandler OnCarry;

    [SerializeField] private Transform logPrefab;
    [SerializeField] private Transform carryPosition;

    private Vector3 hitNormal;

    private void Start()
    {
        
    }


    private void CreateLog()
    {
        var logObj = Instantiate(logPrefab);
        logObj.transform.position = carryPosition.position;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
        var log = hit.gameObject.GetComponent<Log>();

        if (log)
            Debug.Log("Carry");
    }
}
