using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Trees : MonoBehaviour
{
    private enum States
    {
        Live,
        Death
    }

    private States currentState;

    [SerializeField] private GameObject stump;
    [SerializeField] private GameObject logPrefab;

    private List<GameObject> logsList;

    private int maxLogCount = 5;

    private float createTimer;
    private float startCreateTimer = 0.1f;
    void Start()
    {
        stump.SetActive(false);
        logsList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case States.Live:
                break;
            case States.Death:
                Death();
                break;
        }
    }

    private void Death()
    {
        if (logsList.Count < maxLogCount)
        {
            createTimer -= Time.deltaTime;
            if (createTimer <= 0)
            {
                CreateLog();
                createTimer = startCreateTimer;
            }
        }
    }

    private void CreateLog()
    {
        var log = Instantiate(logPrefab);
        logsList.Add(log);

        var offsetZ = (float) logsList.Count / 4;
        var offsetPosition = new Vector3(0f, 0f, offsetZ);
        log.transform.position = transform.position + offsetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInParent<PlayerController>())
        {
            Debug.Log("Hitted");
        }
    }
}
