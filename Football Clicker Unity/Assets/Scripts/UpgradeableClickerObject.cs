using Assets.Scripts;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public abstract class UpgradeableClickerObject : MonoBehaviour
{

    public int UpgradeLevel { get; protected set; } = 0;
    public bool IsEnabled { get; protected set; } = false;
    public float UpgradeCost { get { return mUpgradeCost; } private set { mUpgradeCost = value; } }
    public float TimeUntilIncome { get { return mTimeUntilIncome; } private set { mTimeUntilIncome = value; } }
    public bool CanUpgrade { get; private set; } = true;
    public float Income { get; private set; } = 0f;
    public Text UpgradeText;
    public Text LevelText;
    public Text IncomeText;
    public Text TimeText;
    public Image fillImage;
    public Image fillLevelImage;

    [SerializeField]
    protected float mInitialCost = 0.0f;
    [SerializeField]
    protected float mUpgradeCoefficient = 0.0f;
    [SerializeField]
    protected Club mClub;

    protected float mUpgradeCost = 0.0f;
    protected float mTimeUntilIncome = 0.0f;
    [SerializeField]
    protected float mInitialTimeUntilIncome = 0.0f;

    private int mIncomeMultiplyer = 1;

    private float activeTime = 0.0f;

    public UpgradeableClickerObject(float upgradeCost, Club club, float upgradeCoefficient)
    {
        mClub = club;
        mUpgradeCoefficient = upgradeCoefficient;
        UpdateUpgradeCost();
        UpdateUpgradeIncome(0.0f);
    }

    // Use this for initialization
    public virtual void Start()
    {
        UpdateUpgradeCost();
        fillLevelImage.fillAmount = 0.0f;
        fillImage.fillAmount = 0.0f;
        mIncomeMultiplyer = 1;
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
    }

    // Update is called once per second
    void FixedUpdate()
    {

        //if (TimeUntilIncome > 0)
        //{
        //    TimeUntilIncome -= 1f;
        //}
    }

    public virtual void OnIncomeClick()
    {
        if (UpgradeLevel > 0 && TimeUntilIncome <= 0 && IsEnabled)
        {
            AnalyticsEvent.ItemAcquired(AcquisitionType.Soft, "Money", Income, "Income", mClub.Money);
            mClub.UpdateMoney(Income);
            UpdateIncomeTime();
        }
    }

    public virtual void OnUpgradeClick()
    {
        if (IsEnabled && CurrencyResources.CanAfford(mClub.Money, mUpgradeCost))
        {
            mClub.UpdateMoney(-mUpgradeCost);
            UpgradeLevel += 1;
            AnalyticsEvent.LevelUp(UpgradeLevel);
            AnalyticsEvent.ItemSpent(AcquisitionType.Soft, "Player", mUpgradeCost, "Player");
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
        mInitialTimeUntilIncome = mTimeUntilIncome / 1.5f;
    }

    private void UpdateFillLevelImage()
    {
        if (UpgradeLevel % 25 == 0)
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
}
