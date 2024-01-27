using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private const string LOG_ANIMATION_HASH = "LogAnimation";

    private BuyPlace buyPlace;
    private SellPlace sellPlace;
    private Animator animator;

    [SerializeField] private Image logImage;

    private int logCount = 0;

    public bool canSell = false;
    private void Awake()
    {
        buyPlace = GetComponentInChildren<BuyPlace>();
        sellPlace = GetComponentInChildren<SellPlace>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        SetImageActivate(false);

        buyPlace.OnBuy += IncreaseLogCount;
        sellPlace.OnSell += DecreaseLogCount;
    }

    private void Update()
    {
        if (logCount > 0)
            canSell = true;
        else
            canSell = false;
    }

    private void DecreaseLogCount(object sender, System.EventArgs e)
    {
        logCount--;
    }
    private void IncreaseLogCount(object sender, System.EventArgs e)
    {
        logCount++;
    }

    public void SetImageActivate(bool isActive)
    {
        logImage.gameObject.SetActive(isActive);
        LogAnimationActivate(isActive);
    }

    private void LogAnimationActivate(bool isActive)
    {
        animator.SetBool(LOG_ANIMATION_HASH, isActive);
    }

    public void BackIdleAnimation()
    {
        animator.SetBool(LOG_ANIMATION_HASH, false);
    }
}
