using UnityEngine;

public abstract class UpgradeableClickerObject : MonoBehaviour
{

    public int UpgradeLevel { get; protected set; } = 0;
    public bool IsEnabled { get; protected set; } = false;
    public float UpgradeCost { get { return mUpgradeCost; } private set { mUpgradeCost = value; } }
    public float TimeUntilIncome { get; private set; } = 0;
    public bool CanUpgrade { get; private set; } = true;
    public float Income { get; private set; } = 0f;
    public UnityEngine.UI.Text UpgradeText;
    public UnityEngine.UI.Text IncomeText;

    [SerializeField]
    protected float mUpgradeCost = 0f;
    [SerializeField]
    protected Club mClub;
    private float mNewCost = 0f;

    public UpgradeableClickerObject(float upgradeCost, Club club)
    {
        mClub = club;
        mUpgradeCost = upgradeCost;
        UpdateUpgradeIncome(0.0f, 0.0f);
    }

    // Use this for initialization
    public virtual void Start()
    {
        UpdateUpgradeIncome(0.0f, 0.0f);
    }

    // Update is called once per frame

    public virtual void Update()
    {
        UpdateText();
    }

    // Update is called once per second
    void FixedUpdate()
    {

        if (TimeUntilIncome > 0)
        {
            TimeUntilIncome -= 1f;
        }
    }

    public virtual void OnIncomeClick()
    {
        if (UpgradeLevel > 0 && TimeUntilIncome <= 0 && IsEnabled)
        {
            mClub.UpdateMoney(Income);
            UpdateIncomeTime(0.003f);
        }
    }

    public virtual void OnUpgradeClick()
    {
        if (IsEnabled)
        {
            mClub.UpdateMoney(-mUpgradeCost);
            UpdateUpgradeCost(1.15f);
            UpdateUpgradeIncome(0.05f, 0.003f);
            UpgradeLevel += 1;
        }
    }

    protected void UpdateUpgradeCost(float upgradeValue)
    {
        mUpgradeCost = Mathf.Round(mUpgradeCost * upgradeValue);
        mNewCost = Mathf.Pow(mUpgradeCost, mNewCost = mUpgradeCost);
    }

    protected void UpdateUpgradeIncome(float incomeValue, float timeValue)
    {
        Income = Mathf.Round(mUpgradeCost * incomeValue);
        UpdateIncomeTime(timeValue);
    }

    protected void UpdateIncomeTime(float value)
    {
        TimeUntilIncome = Mathf.Round(mUpgradeCost * value);
    }

    public virtual void UpdateText()
    {
        UpgradeText.text = $"Level: {UpgradeLevel} Cost:{UpgradeCost}";
        IncomeText.text = $"Income: {Income} Time Until: {TimeUntilIncome}";
    }
}
