using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCarry : MonoBehaviour
{
    private event EventHandler OnCarry;

    [SerializeField] private Transform logPrefab;
    [SerializeField] private Transform carryPosition;

    private List<GameObject> logList;

    private void Start()
    {
        logList = new List<GameObject>();

        OnCarry += CreateLog;
    }

    private void CreateLog(object sender, EventArgs e)
    {
        var logObj = Instantiate(logPrefab, carryPosition);
        var offsetY = (float)logList.Count / 4f;

        logObj.transform.position = new Vector3(carryPosition.position.x, carryPosition.position.y + offsetY, carryPosition.position.z);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var log = hit.gameObject.GetComponent<Log>();

        if (log)
        {
            logList.Add(log.gameObject);
            OnCarry?.Invoke(this, EventArgs.Empty);
            Destroy(log.gameObject);
        }
    }
}
