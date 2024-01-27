using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuyPlace : MonoBehaviour
{
    public event EventHandler OnBuy;

    private PlayerCarry player;
    private Building building;

    private float buyTimer;
    private float startBuyTimer = 1f;
    private void Awake()
    {
        building = GetComponentInParent<Building>();
    }

    private void Start()
    {
        buyTimer = startBuyTimer;
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<PlayerCarry>();
    }

    private void OnTriggerStay(Collider other)
    {
        var isCarry = player.GetIsCarry();

        if (player && isCarry)
        {
            buyTimer -= Time.deltaTime;

            if (buyTimer <= 0)
            {
                OnBuy?.Invoke(this, EventArgs.Empty);
                buyTimer = startBuyTimer;
                player.RemoveLastLog();
            }
        }
        else
        {
            building.SetImageActivate(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
        building.SetImageActivate(false);
        
    }
}
