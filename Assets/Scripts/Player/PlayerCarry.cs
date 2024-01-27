using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCarry : MonoBehaviour
{
    private event EventHandler OnCarry;

    [SerializeField] private Transform carryPosition;

    private List<GameObject> logList;

    private bool isCarry;

    private void Start()
    {
        logList = new List<GameObject>();

    }

    private void Update()
    {
        if (logList.Count <= 0)
            isCarry = false;
        else
            isCarry = true;

    }

    public void RemoveLastLog()
    {
        var lastLogIndex = logList.Count - 1;
        var lastLog = logList[lastLogIndex];
        Destroy(lastLog);
        logList.RemoveAt(lastLogIndex);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var log = hit.gameObject.GetComponent<Log>();

        if (log)
        {
            logList.Add(log.gameObject);
            OnCarry?.Invoke(this, EventArgs.Empty);
            log.transform.SetParent(carryPosition);

            var offsetY = (float)logList.Count / 4f;
            log.transform.position = new Vector3(carryPosition.position.x, carryPosition.position.y + offsetY, carryPosition.position.z);
            log.transform.rotation = carryPosition.transform.rotation;
        }
    }

    public bool GetIsCarry()
    {
        return isCarry;
    }
}
