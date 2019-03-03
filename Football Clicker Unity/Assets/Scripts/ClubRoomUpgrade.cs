using Assets.Scripts;
using UnityEngine;
using UnityEngine.Analytics;

public class ClubRoomUpgrade : MonoBehaviour
{

    [SerializeField]
    protected UnityEngine.UI.Text PurchaseText;
    [SerializeField]
    protected UnityEngine.UI.Text TicketPurchaseText;
    [SerializeField]
    protected UnityEngine.UI.Image PurchaseButton;
    [SerializeField]
    protected UnityEngine.UI.Image TicketPurchaseButton;

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
    [SerializeField]
    protected int mTicketPurchaseCost;

    // Use this for initialization
    public virtual void Start()
    {
        PurchaseText.text = "Purchase";
        TicketPurchaseText.text = mTicketPurchaseCost.ToString();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!mIsOwned && !mIsEnabled && mObjectToManage.UpgradeLevel >= mUnlockLevel)
        {
            mPurchaseCost = Mathf.Round(mObjectToManage.UpgradeCost * 5.15f);
            PurchaseText.text = $"Purchase: {CurrencyResources.CurrencyToString(mPurchaseCost, true)}";
            TicketPurchaseText.text = mTicketPurchaseCost.ToString();
            mIsEnabled = true;
        }
    }

    // Update is called once per second
    public virtual void FixedUpdate()
    {

    }

    public void PurchaseClick()
    {
        if (mIsEnabled && mObjectToManage.UpgradeLevel > 0 && CurrencyResources.CanAfford(mClub.Money, mPurchaseCost))
        {
            mIsEnabled = false;
            mClub.UpdateMoney(-mPurchaseCost);
            AnalyticsEvent.ItemSpent(AcquisitionType.Soft, "Upgrade", mPurchaseCost, "Upgrade");
            PurchaseText.text = "Owned";
            PurchaseButton.color = Color.red;
            TicketPurchaseButton.color = Color.red;
            mIsOwned = true;
            mObjectToManage.UpdateIncomeMultiplyer(2);
        }
    }

    public void TicketPurchaseClick()
    {
        if (mIsEnabled && mObjectToManage.UpgradeLevel > 0 && CurrencyResources.CanAfford(mClub.Tickets, mTicketPurchaseCost))
        {
            mIsEnabled = false;
            mClub.UpdateTickets(-mTicketPurchaseCost);
            AnalyticsEvent.ItemSpent(AcquisitionType.Premium, "Upgrade", mTicketPurchaseCost, "Upgrade");
            PurchaseText.text = "Owned";
            PurchaseButton.color = Color.red;
            TicketPurchaseButton.color = Color.red;
            mIsOwned = true;
            mObjectToManage.UpdateIncomeMultiplyer(2);
        }
    }
}
