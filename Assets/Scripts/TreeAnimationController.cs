using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimationController : MonoBehaviour
{
    private Trees tree;
    private TreeHealthManager healthManager;

    private Animator animator;
    private void Awake()
    {
        tree = GetComponent<Trees>();
        healthManager = GetComponent<TreeHealthManager>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        tree.OnHit += Tree_OnHit;
        healthManager.OnDeath += Tree_OnDeath;
    }

    private void Tree_OnDeath(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Death");
    }

    private void Tree_OnHit(object sender, System.EventArgs e)
    {
        animator.SetBool("Hit", true);
    }

    private void ResetHitAnimation()
    {
        animator.SetBool("Hit", false);
    }
}
