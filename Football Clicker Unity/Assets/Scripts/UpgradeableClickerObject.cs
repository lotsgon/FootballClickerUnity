using Assets.Scripts;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

[System.Serializable]
public class UpgradeableClickerObjectData
{
    public int UpgradeLevel;
    public bool IsEnabled;
    public float UpgradeCost;
    public float InitialCost;
    public float UpgradeCoefficient;
    public int IncomeMultiplyer;
    public float TimeUntilIncome;
    public float ActiveTime;
    public bool CanUpgrade;
    public float Income;
    public string ItemID;
    public float LevelFillAmount;
    public float IncomeFillAmount;
    public float InitialTimeUntilIncome;

    public UpgradeableClickerObjectData(UpgradeableClickerObject upgradeableClickerObject)
    {
        UpgradeLevel = upgradeableClickerObject.UpgradeLevel;
        IsEnabled = upgradeableClickerObject.IsEnabled;
        UpgradeCost = upgradeableClickerObject.UpgradeCost;
        TimeUntilIncome = upgradeableClickerObject.TimeUntilIncome;
        CanUpgrade = upgradeableClickerObject.CanUpgrade;
        Income = upgradeableClickerObject.Income;
        ItemID = upgradeableClickerObject.ItemID;
        InitialCost = upgradeableClickerObject.InitialCost;
        InitialTimeUntilIncome = upgradeableClickerObject.InitialTimeUntilIncome;
        UpgradeCoefficient = upgradeableClickerObject.UpgradeCoefficient;
        IncomeMultiplyer = upgradeableClickerObject.IncomeMultiplyer;
        ActiveTime = upgradeableClickerObject.ActiveTime;
        LevelFillAmount = upgradeableClickerObject.fillLevelImage.fillAmount;
    }
}

public abstract class UpgradeableClickerObject : MonoBehaviour
{

    public int UpgradeLevel { get; protected set; } = 0;
    public bool IsEnabled { get; protected set; } = false;
    public float UpgradeCost { get { return mUpgradeCost; } private set { mUpgradeCost = value; } }
    public float InitialCost { get { return mInitialCost; } private set { mInitialCost = value; } }
    public float UpgradeCoefficient { get { return mUpgradeCoefficient; } private set { mUpgradeCoefficient = value; } }
    public int IncomeMultiplyer { get { return mIncomeMultiplyer; } private set { mIncomeMultiplyer = value; } }
    public float TimeUntilIncome { get { return mTimeUntilIncome; } private set { mTimeUntilIncome = value; } }
    public float InitialTimeUntilIncome { get { return mInitialTimeUntilIncome; } private set { mInitialTimeUntilIncome = value; } }
    public float ActiveTime { get { return activeTime; } private set { activeTime = value; } }
    public bool CanUpgrade { get; private set; } = true;
    public float Income { get; private set; } = 0f;

    public Text UpgradeText;
    public Text LevelText;
    public Text IncomeText;
    public Text TimeText;
    public Image fillImage;
    public Image fillLevelImage;
    public string ItemID;
    public Button UpgradeIconButton;
    public Button IncomeButton;
    public Button UpgradeCostButton;

    [SerializeField]
    protected float mInitialCost;
    [SerializeField]
    protected float mUpgradeCoefficient;
    [SerializeField]
    protected Club mClub;

    protected float mUpgradeCost;
    protected float mTimeUntilIncome;
    [SerializeField]
    protected float mInitialTimeUntilIncome = 0.0f;

    private int mIncomeMultiplyer = 1;

    private float activeTime = 0.0f;

    public UpgradeableClickerObject(float upgradeCost, Club club, float upgradeCoefficient, float initalIncomeTime)
    {
        mClub = club;
        mUpgradeCoefficient = upgradeCoefficient;
        mInitialTimeUntilIncome = initalIncomeTime;
        UpdateUpgradeCost();
        UpdateUpgradeIncome(0.0f);
    }

    protected UpgradeableClickerObject()
    {
    }

    // Use this for initialization
    public virtual void Start()
    {
        UpdateUpgradeCost();
        UpdateFillLevelImage();
        //fillLevelImage.fillAmount = 0.0f;
        //fillImage.fillAmount = 0.0f;
        //mIncomeMultiplyer = 1;
    }


    // Update is called once per frame

    public virtual void Update()
    {
        if (TimeUntilIncome > 0)
        {
            TimeUntilIncome -= Time.deltaTime;
            UpdateFillImage();
        }
        UpdateText();

        if(mClub.Money >= mUpgradeCost)
        {
            UpgradeIconButton.interactable = true;
        }
        else
        {
            UpgradeIconButton.interactable = false;
        }

        if (IsEnabled)
        {
            IncomeButton.interactable = true;
            UpgradeCostButton.interactable = true;
        }
        else
        {
            IncomeButton.interactable = false;
            UpgradeCostButton.interactable = false;
            fillImage.fillAmount = 0.0f;
        }
    }

    public void SetUpgradeableClickerObjectData(UpgradeableClickerObjectData upgradeableClickerObjectData)
    {
        UpgradeLevel = upgradeableClickerObjectData.UpgradeLevel;
        IsEnabled = upgradeableClickerObjectData.IsEnabled;
        UpgradeCost = upgradeableClickerObjectData.UpgradeCost;
        TimeUntilIncome = upgradeableClickerObjectData.TimeUntilIncome;
        InitialTimeUntilIncome = upgradeableClickerObjectData.InitialTimeUntilIncome;
        CanUpgrade = upgradeableClickerObjectData.CanUpgrade;
        Income = upgradeableClickerObjectData.Income;
        ItemID = upgradeableClickerObjectData.ItemID;
        mInitialCost = upgradeableClickerObjectData.InitialCost;
        mUpgradeCoefficient = upgradeableClickerObjectData.UpgradeCoefficient;
        mUpgradeCost = upgradeableClickerObjectData.UpgradeCost;
        mTimeUntilIncome = upgradeableClickerObjectData.TimeUntilIncome;
        mInitialTimeUntilIncome = upgradeableClickerObjectData.InitialTimeUntilIncome;
        mIncomeMultiplyer = upgradeableClickerObjectData.IncomeMultiplyer;
        activeTime = upgradeableClickerObjectData.ActiveTime;
        fillLevelImage.fillAmount = upgradeableClickerObjectData.LevelFillAmount;
    }

    public virtual void OnIncomeClick()
    {
        if (UpgradeLevel > 0 && TimeUntilIncome <= 0 && IsEnabled)
        {
            //AnalyticsEvent.ItemAcquired(AcquisitionType.Soft, "Income", Income, ItemID, mClub.Money);
            mClub.UpdateMoney(Income);
            UpdateIncomeTime();
        }
    }

    public virtual void OnUpgradeClick()
    {
        if (CurrencyResources.CanAfford(mClub.Money, mUpgradeCost))
        {
            mClub.UpdateMoney(-mUpgradeCost);
            UpgradeLevel += 1;
            AnalyticsEvent.LevelUp(UpgradeLevel);
            //AnalyticsEvent.ItemSpent(AcquisitionType.Soft, "PlayerUpgrade", mUpgradeCost, ItemID);
            UpdateFillLevelImage();
            UpdateUpgradeIncome(2.25f);
            UpdateUpgradeCost();
        }
    }

    protected void UpdateUpgradeCost()
    {
        mUpgradeCost = mInitialCost * Mathf.Pow(mUpgradeCoefficient, UpgradeLevel - 1);
    }

    protected void UpdateUpgradeIncome(float incomeValue)
    {
        Income = (mUpgradeCost / incomeValue) * mIncomeMultiplyer;
        UpdateIncomeTime();
    }

    public void UpdateIncomeMultiplyer(int multiplyer)
    {
        mIncomeMultiplyer *= multiplyer;
        UpdateUpgradeIncome(2.25f);
    }

    protected void UpdateIncomeTime()
    {
        TimeUntilIncome = mInitialTimeUntilIncome;
        activeTime = 0.0f;
        fillImage.fillAmount = 0.0f;
    }

    public virtual void UpdateText()
    {
        LevelText.text = $"LEVEL {UpgradeLevel}";
        UpgradeText.text = CurrencyResources.CurrencyToString(UpgradeCost, true);
        IncomeText.text = CurrencyResources.CurrencyToString(Income, true);
        if (TimeUntilIncome > 59)
        {
            var time = new CountdownTime(Mathf.Round(TimeUntilIncome));
            TimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        }
        else
        {
            TimeText.text = string.Format("00:00:{0:D2}", Mathf.RoundToInt(TimeUntilIncome));
        }
    }

    private void UpdateFillImage()
    {
            activeTime += Time.deltaTime;
            var percent = activeTime / mInitialTimeUntilIncome;
            fillImage.fillAmount = Mathf.Lerp(0.0f, 1.0f, percent);
    }

    private void UpdateLevelMultiplyer()
    {
        mInitialTimeUntilIncome = Mathf.Max(0.3f, mTimeUntilIncome / 1.5f);
    }

    protected void UpdateFillLevelImage()
    {
        if(UpgradeLevel == 0)
        {
            fillLevelImage.fillAmount = 0.0f;
        }
        else if (UpgradeLevel % 25 == 0)
        {
            fillLevelImage.fillAmount += Mathf.Lerp(0, 1, 1.0f / 25.0f);
            UpdateLevelMultiplyer();
        }
        else if (UpgradeLevel % 25 == 1)
        {
            fillLevelImage.fillAmount = 0.0f;
            fillLevelImage.fillAmount += Mathf.Lerp(0, 1, 1.0f / 25.0f);
        }
        else
        {
            fillLevelImage.fillAmount += Mathf.Lerp(0, 1, 1.0f / 25.0f);
        }
    }

    private void OnApplicationQuit()
    {
        
    }
}
