using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Trees : MonoBehaviour
{
    public event EventHandler OnHit;

    private PlayerController player;
    private TreeHealthManager healthManager;
    private CapsuleCollider capsuleCollider;

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
    private float delayTimer = 1f;
    private void Awake()
    {
        healthManager = GetComponent<TreeHealthManager>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    void Start()
    {
        stump.SetActive(false);
        logsList = new List<GameObject>();
        healthManager.OnDeath += HealthManager_OnDeath;
    }

    private void HealthManager_OnDeath(object sender, EventArgs e)
    {
        currentState = States.Death;
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
        capsuleCollider.enabled = false;

        delayTimer -= Time.deltaTime;
        if (delayTimer <= 0)
        {
            stump.gameObject.SetActive(false);
            if (logsList.Count < maxLogCount)
            {
                createTimer -= Time.deltaTime;
                if (createTimer <= 0)
                {
                    CreateLog();
                    createTimer = startCreateTimer;
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void CreateLog()
    {
        var log = Instantiate(logPrefab);
        logsList.Add(log);

        var offsetZ = (float) logsList.Count / 4;
        var offsetPosition = new Vector3(0f, 0f, offsetZ);
        log.transform.position = stump.transform.position + offsetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponentInParent<PlayerController>();
        if(player)
        {
            OnHit?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
    }

    public void ActivateStump()
    {
        stump.SetActive(true);
    }
}
