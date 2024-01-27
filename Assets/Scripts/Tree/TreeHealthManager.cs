using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeHealthManager : MonoBehaviour
{
    public event EventHandler OnDeath;

    private Trees tree;

    private int maximumHealth = 4;
    private int currentHealth;
    void Start()
    {
        tree = GetComponent<Trees>();
        currentHealth = maximumHealth;

        tree.OnHit += DecreaseHealth;
    }
    private void OnDestroy()
    {
        tree.OnHit -= DecreaseHealth;
    }

    private void DecreaseHealth(object sender, System.EventArgs e)
    {
        var damage = 1;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            tree.ActivateStump();
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

}
