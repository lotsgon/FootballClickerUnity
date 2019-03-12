using Assets.Scripts;
using UnityEngine;
using UnityEngine.Analytics;

[System.Serializable]
public class ClubRoomUpgradeData
{
    public int mUnlockLevel;
    public bool mIsEnabled;
    public bool mIsOwned;
    public float mPurchaseCost;
    public int mTicketPurchaseCost;
    public string mPosition;

    public ClubRoomUpgradeData(ClubRoomUpgrade upgrade)
    {
        mUnlockLevel = upgrade.UnlockLevel;
        mIsEnabled = upgrade.IsEnabled;
        mIsOwned = upgrade.IsOwned;
        mPurchaseCost = upgrade.PurchaseCost;
        mTicketPurchaseCost = upgrade.TicketPurchaseCost;
        mPosition = upgrade.Position;
    }
}

public class ClubRoomUpgrade : MonoBehaviour
{
    public string Position { get { return mPosition; } set { mPosition = value; } }
    public int UnlockLevel { get { return mUnlockLevel; } set { mUnlockLevel = value; } }
    public bool IsEnabled { get { return mIsEnabled; } set { mIsEnabled = value; } }
    public bool IsOwned { get { return mIsOwned; } set { mIsOwned = value; } }
    public float PurchaseCost { get { return mPurchaseCost; } set { mPurchaseCost = value; } }
    public int TicketPurchaseCost { get { return mTicketPurchaseCost; } set { mTicketPurchaseCost = value; } }

    [SerializeField]
    protected UnityEngine.UI.Text PurchaseText;
    [SerializeField]
    protected UnityEngine.UI.Text TicketPurchaseText;
    [SerializeField]
    protected UnityEngine.UI.Image PurchaseButtonImage;
    [SerializeField]
    protected UnityEngine.UI.Image TicketPurchaseButtonImage;

    [SerializeField]
    private UnityEngine.UI.Button TicketPurchaseButton;
    [SerializeField]
    private UnityEngine.UI.Button PurchaseButton;

    [SerializeField]
    protected int mUnlockLevel;
    protected bool mIsEnabled;
    protected bool mIsOwned;
    [SerializeField]
    protected UpgradeableClickerObject mObjectToManage;
    [SerializeField]
    protected Club mClub;
    [SerializeField]
    protected float mPurchaseCost;
    [SerializeField]
    protected int mTicketPurchaseCost;
    [SerializeField]
    private string mPosition;

    public ClubRoomUpgrade()
    {
        mIsEnabled = false;
        mIsOwned = false;
    }

    // Use this for initialization
    public virtual void Start()
    {
        PurchaseText.text = "Purchase";
        TicketPurchaseText.text = mTicketPurchaseCost.ToString();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!mIsOwned && mObjectToManage.UpgradeLevel >= mUnlockLevel)
        {
            mPurchaseCost = Mathf.Round(mObjectToManage.UpgradeCost * 5.15f);
            PurchaseText.text = $"Purchase: {CurrencyResources.CurrencyToString(mPurchaseCost, true)}";
            TicketPurchaseText.text = mTicketPurchaseCost.ToString();
            mIsEnabled = true;
        }

        if (!mIsOwned)
        {
            if (mClub.Tickets >= mTicketPurchaseCost && mObjectToManage.IsEnabled)
            {
                TicketPurchaseButton.interactable = true;
            }
            else
            {
                TicketPurchaseButton.interactable = false;
            }

            if (mClub.Money >= PurchaseCost && mObjectToManage.IsEnabled)
            {
                PurchaseButton.interactable = true;
            }
            else
            {
                PurchaseButton.interactable = false;
            }
        }
        else
        {
            PurchaseButton.interactable = false;
            TicketPurchaseButton.interactable = false;
            PurchaseText.text = "Owned";
            PurchaseButtonImage.color = Color.red;
        }


    }

    public void SetClubRoomUpgradeData(ClubRoomUpgradeData upgradeData)
    {
        mUnlockLevel = upgradeData.mUnlockLevel;
        mIsEnabled = upgradeData.mIsEnabled;
        mIsOwned = upgradeData.mIsOwned;
        mPurchaseCost = upgradeData.mPurchaseCost;
        mTicketPurchaseCost = upgradeData.mTicketPurchaseCost;
        mPosition = upgradeData.mPosition;
    }

    // Update is called once per second
    public virtual void FixedUpdate()
    {

    }

    public void PurchaseClick()
    {
        if (mObjectToManage.UpgradeLevel > 0 && CurrencyResources.CanAfford(mClub.Money, mPurchaseCost) && !mIsOwned)
        {
            mIsEnabled = false;
            mClub.UpdateMoney(-mPurchaseCost);
            AnalyticsEvent.ItemSpent(AcquisitionType.Soft, "Upgrade-Pink-2", mPurchaseCost, mPosition);
            PurchaseText.text = "Owned";
            PurchaseButtonImage.color = Color.red;
            TicketPurchaseButtonImage.color = Color.red;
            mIsOwned = true;
            mObjectToManage.UpdateIncomeMultiplyer(2);
        }
    }

    public void TicketPurchaseClick()
    {
        if (mObjectToManage.UpgradeLevel > 0 && CurrencyResources.CanAfford(mClub.Tickets, mTicketPurchaseCost) && !mIsOwned)
        {
            mIsEnabled = false;
            mClub.UpdateTickets(-mTicketPurchaseCost);
            AnalyticsEvent.ItemSpent(AcquisitionType.Premium, "Upgrade-Pink-2", mTicketPurchaseCost, mPosition);
            PurchaseText.text = "Owned";
            PurchaseButtonImage.color = Color.red;
            TicketPurchaseButtonImage.color = Color.red;
            mIsOwned = true;
            mObjectToManage.UpdateIncomeMultiplyer(2);
        }
    }

    private void OnApplicationPause()
    {
        SaveLoadManager.SaveUpgrade(this);
    }

    private void OnApplicationQuit()
    {
        SaveLoadManager.SaveUpgrade(this);
    }
}
