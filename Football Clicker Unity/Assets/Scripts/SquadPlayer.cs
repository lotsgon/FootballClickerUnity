using Assets.Scripts;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

[System.Serializable]
public class SquadPlayerData
{
    public string position;
    public float saleValue;
    public UpgradeableClickerObjectData upgradeableClickerObjectData;

    public SquadPlayerData(SquadPlayer player)
    {
        position = player.Position;
        saleValue = player.SaleValue;
        upgradeableClickerObjectData = new UpgradeableClickerObjectData(player);
    }
}

public class SquadPlayer : UpgradeableClickerObject
{

    public string Position { get { return mPosition; } set { mPosition = value; } }
    public float SaleValue { get; private set; }

    [SerializeField]
    private string mPosition;
    private bool triedDataLoad = false;

    public SquadPlayer() : base() { }

    public SquadPlayer(float upgradeCost, Club club, float upgradeCoefficient, string postition, float initalIncomeTime) : base(upgradeCost, club, upgradeCoefficient, initalIncomeTime)
    {
        mPosition = postition;
        SaleValue = upgradeCost / 2;
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void SetSquadPlayerData(SquadPlayerData playerData)
    {
        Position = playerData.position;
        SaleValue = playerData.saleValue;
        SetUpgradeableClickerObjectData(playerData.upgradeableClickerObjectData);
    }

    public void OnSellClick()
    {
        if (UpgradeLevel > 0)
        {
            mClub.UpdateMoney(SaleValue);
            UpdateUpgradeIncome(0.0f);
            UpgradeLevel = 0;
            base.UpdateUpgradeCost();
        }
    }

    public override void OnIncomeClick()
    {
        if (UpgradeLevel > 0 && TimeUntilIncome <= 0 && IsEnabled)
        {
            //AnalyticsEvent.ItemAcquired(AcquisitionType.Soft, "Income", Income, mPosition, mClub.Money);
            mClub.UpdateMoney(Income);
            UpdateIncomeTime();
        }
    }

    public override void OnUpgradeClick()
    {
        if (CurrencyResources.CanAfford(mClub.Money, mUpgradeCost))
        {
            mClub.UpdateMoney(-mUpgradeCost);
            UpgradeLevel += 1;
            if (UpgradeLevel == 1 || UpgradeLevel % 25 == 0)
            {
                AnalyticsEvent.ItemSpent(AcquisitionType.Soft, "PlayerUpgrade-Blue-1s", mUpgradeCost, mPosition);
                AnalyticsEvent.LevelUp(mPosition, UpgradeLevel);
            }
            UpdateFillLevelImage();
            UpdateUpgradeIncome(2.25f);
            UpdateUpgradeCost();
            SaleValue = mUpgradeCost / 2;
            IsEnabled = true;
        }
    }

    public float CalculateMoneyEarntWhileAway()
    {
        if (this.Income == 0)
        { return 0.0f; }

        var manager = UnityEngine.Object.FindObjectsOfType<AutomatedManagerObject>().Where(x => x.Position == mPosition).FirstOrDefault();

        if (manager == null || !manager.IsOwned)
        {
            return 0.0f;
        }

        var saveTime = DateTime.FromFileTime(mClub.SaveTime);

        TimeSpan timeGone = DateTime.Now - saveTime;

        if (timeGone.TotalSeconds < 1)
        { return 0.0f; }

        var timesCompleted = (int)(timeGone.TotalSeconds / mInitialTimeUntilIncome);

        mTimeUntilIncome = (float)(timeGone.TotalSeconds % mInitialTimeUntilIncome);

        return timesCompleted * Income;
    }

    private void OnApplicationPause()
    {
        SaveLoadManager.SaveSquadPlayer(this);
    }

    private void OnApplicationQuit()
    {
        SaveLoadManager.SaveSquadPlayer(this);
    }
}
