using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SellPlace : MonoBehaviour
{
    public event EventHandler OnSell;

    private Flatbed flatbed;
    private Building building;

    private float sellTimer;
    private float startSellTimer = 1f;
    private void Awake()
    {
        building = GetComponentInParent<Building>();
    }
    private void Start()
    {
        sellTimer = startSellTimer;
    }

    private void OnTriggerEnter(Collider other)
    {
        flatbed = other.gameObject.GetComponent<Flatbed>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (flatbed && building.canSell)
        {
            sellTimer -= Time.deltaTime;

            if (sellTimer <= 0)
            {
                OnSell?.Invoke(this, EventArgs.Empty);
                sellTimer = startSellTimer;
                flatbed.DecreaseDesiredLogCount();
            }
        }
        else
        {
            sellTimer = startSellTimer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        flatbed = null;
    }
}
