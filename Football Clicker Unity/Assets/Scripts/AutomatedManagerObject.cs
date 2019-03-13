using Assets.Scripts;
using UnityEngine;
using UnityEngine.Analytics;

[System.Serializable]
public class AutomatedManagerObjectData
{
    public int mUnlockLevel;
    public bool mIsEnabled;
    public bool mIsOwned;
    public float mPurchaseCost;
    public string mPosition;

    public AutomatedManagerObjectData(AutomatedManagerObject manager)
    {
        mUnlockLevel = manager.UnlockLevel;
        mIsEnabled = manager.IsEnabled;
        mIsOwned = manager.IsOwned;
        mPurchaseCost = manager.PurchaseCost;
        mPosition = manager.Position;
    }

}

public class AutomatedManagerObject : MonoBehaviour
{

    public string Position { get { return mPosition; } set { mPosition = value; } }
    public int UnlockLevel { get { return mUnlockLevel; } set { mUnlockLevel = value; } }
    public bool IsEnabled { get { return mIsEnabled; } set { mIsEnabled = value; } }
    public bool IsOwned { get { return mIsOwned; } set { mIsOwned = value; } }
    public float PurchaseCost { get { return mPurchaseCost; } set { mPurchaseCost = value; } }

    [SerializeField]
    protected UnityEngine.UI.Text PurchaseText;
    [SerializeField]
    protected UnityEngine.UI.Image PurchaseImage;

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
    private string mPosition;

    public AutomatedManagerObject()
    {
        mIsEnabled = false;
        mIsOwned = false;
    }

    // Use this for initialization
    public virtual void Start()
    {

        PurchaseText.text = "Purchase";
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!mIsOwned && mObjectToManage.UpgradeLevel >= mUnlockLevel)
        {
            mPurchaseCost = Mathf.Round(mObjectToManage.UpgradeCost * 5.15f);
            PurchaseText.text = $"Purchase: {CurrencyResources.CurrencyToString(mPurchaseCost, true)}";
            mIsEnabled = true;
        }

        if (mIsOwned && mObjectToManage.TimeUntilIncome <= 0)
        {
            mObjectToManage.OnIncomeClick();
        }

        if (!mIsOwned)
        {

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
            PurchaseText.text = "Owned";
            PurchaseImage.color = Color.red;
        }

    }

    public void SetAutomatedManagerObjectData(AutomatedManagerObjectData managerData)
    {
        mUnlockLevel = managerData.mUnlockLevel;
        mIsEnabled = managerData.mIsEnabled;
        mIsOwned = managerData.mIsOwned;
        mPurchaseCost = managerData.mPurchaseCost;
        mPosition = managerData.mPosition;
    }

    // Update is called once per second
    public virtual void FixedUpdate()
    {

    }

    public void PurchaseClick()
    {
        if (mObjectToManage.UpgradeLevel > 0 && CurrencyResources.CanAfford(mClub.Money, mPurchaseCost) && !mIsOwned)
        {
            AnalyticsEvent.ItemSpent(AcquisitionType.Soft, "Manager-Green-3", mPurchaseCost, mPosition);
            mIsEnabled = false;
            mClub.UpdateMoney(-mPurchaseCost);
            PurchaseText.text = "Owned";
            PurchaseImage.color = Color.red;
            mIsOwned = true;
        }
    }

    private void OnApplicationPause()
    {
        SaveLoadManager.SaveAutomatedManager(this);
    }

    private void OnApplicationQuit()
    {
        SaveLoadManager.SaveAutomatedManager(this);
    }
}
