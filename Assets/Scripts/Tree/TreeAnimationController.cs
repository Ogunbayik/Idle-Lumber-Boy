using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimationController : MonoBehaviour
{
    private const string DEATH_ANIMATON_HASH = "Death";
    private const string HIT_ANIMATION_HASH = "Hit";

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
        animator.SetTrigger(DEATH_ANIMATON_HASH);
    }

    private void Tree_OnHit(object sender, System.EventArgs e)
    {
        animator.SetBool(HIT_ANIMATION_HASH, true);
    }

    private void ResetHitAnimation()
    {
        animator.SetBool(HIT_ANIMATION_HASH, false);
    }
}
