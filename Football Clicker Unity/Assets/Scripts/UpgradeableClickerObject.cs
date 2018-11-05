using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeableClickerObject : MonoBehaviour
{

    public int UpgradeLevel { get; protected set; } = 0;
    public bool IsEnabled { get; protected set; } = false;
    public float UpgradeCost { get { return mUpgradeCost; } private set { mUpgradeCost = value; } }
    public float TimeUntilIncome { get; private set; } = 0;
    public bool CanUpgrade { get; private set; } = true;
    public float Income { get; private set; } = 0f;
    public Text UpgradeText;
    public Text LevelText;
    public Text IncomeText;
    public Text TimeText;
    public Image fillImage;

    [SerializeField]
    protected float mUpgradeCost = 0f;
    [SerializeField]
    protected Club mClub;
    private float fillTime;

    public UpgradeableClickerObject(float upgradeCost, Club club)
    {
        mClub = club;
        mUpgradeCost = upgradeCost;
        UpdateUpgradeIncome(0.0f, 0.0f);
    }

    // Use this for initialization
    public abstract void Start();

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
    }

    protected void UpdateUpgradeIncome(float incomeValue, float timeValue)
    {
        Income = Mathf.Round(mUpgradeCost * incomeValue);
        UpdateIncomeTime(timeValue);
    }

    protected void UpdateIncomeTime(float value)
    {
        TimeUntilIncome = Mathf.Round(mUpgradeCost * value);
        fillTime = 0.0f;
        fillImage.fillAmount = 0.0f;
    }

    public virtual void UpdateText()
    {
        LevelText.text = $"LEVEL {UpgradeLevel}";
        UpgradeText.text = UpgradeCost.ToString("C2");
        IncomeText.text = Income.ToString("C2");
        if (TimeUntilIncome > 59)
        {
            var time = new CountdownTime(TimeUntilIncome);
            TimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        }
        else
        {
            TimeText.text = string.Format("00:00:{0:D2}", Mathf.RoundToInt(TimeUntilIncome));
        }
    }

    private void UpdateFillImage()
    {
            var percent = 1.0f / TimeUntilIncome * Time.deltaTime/6;
            fillImage.fillAmount += Mathf.Lerp(0, 1, percent);
    }
}
