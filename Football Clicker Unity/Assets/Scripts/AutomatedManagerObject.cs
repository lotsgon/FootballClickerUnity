using Assets.Scripts;
using UnityEngine;
using UnityEngine.Analytics;

public class AutomatedManagerObject : MonoBehaviour {

    [SerializeField]
    protected UnityEngine.UI.Text PurchaseText;
    [SerializeField]
    protected UnityEngine.UI.Image PurchaseButton;

    [SerializeField]
    protected int mUnlockLevel;
    protected bool mIsEnabled = false;
    protected bool mIsOwned = false;
    [SerializeField]
    protected UpgradeableClickerObject mObjectToManage;
    [SerializeField]
    protected Club mClub;
    [SerializeField]
    protected float mPurchaseCost;

    // Use this for initialization
    public virtual void Start () {
        PurchaseText.text = "Purchase";
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(!mIsOwned && !mIsEnabled && mObjectToManage.UpgradeLevel >= mUnlockLevel)
        {
            mPurchaseCost = Mathf.Round(mObjectToManage.UpgradeCost * 5.15f);
            PurchaseText.text = $"Purchase: {CurrencyResources.CurrencyToString(mPurchaseCost, true)}";
            mIsEnabled = true;
        }

        if (mIsOwned && mObjectToManage.TimeUntilIncome <= 0)
        {
            mObjectToManage.OnIncomeClick();
        }

    }

    // Update is called once per second
    public virtual void FixedUpdate () {
		
	}

    public void PurchaseClick()
    {
        if (mIsEnabled && mObjectToManage.UpgradeLevel > 0 && CurrencyResources.CanAfford(mClub.Money, mPurchaseCost))
        {
            AnalyticsEvent.ItemSpent(AcquisitionType.Soft, "Manager", mPurchaseCost, "Manager");
            mIsEnabled = false;
            mClub.UpdateMoney(-mPurchaseCost);
            PurchaseText.text = "Owned";
            PurchaseButton.color = Color.red;
            mIsOwned = true;
        }
    }
}
