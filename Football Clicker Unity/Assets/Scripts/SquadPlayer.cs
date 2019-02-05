using UnityEngine;

public class SquadPlayer : UpgradeableClickerObject {

    public string Position { get { return mPosition; } set { mPosition = value; } }
    public float SaleValue { get; private set; }

    [SerializeField]
    private string mPosition;
    [SerializeField]
    private TeamManager mTeamManager;

    public SquadPlayer(float upgradeCost, Club club, float upgradeCoefficient, string postition, TeamManager manager) : base(upgradeCost, club, upgradeCoefficient)
    {
        mPosition = postition;
        SaleValue = upgradeCost / 2;
        mTeamManager = manager;
    }

    // Use this for initialization
    public override void Start()
    {
        base.IsEnabled = true;
        base.Start();
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
    }

    public void OnSellClick()
    {
        if (UpgradeLevel > 0)
        {
            mClub.UpdateMoney(SaleValue);
            mUpgradeCost = mUpgradeCost * mUpgradeCoefficient;
            UpdateUpgradeIncome(0.0f, 0.0f);
            UpgradeLevel = 0;
        }
    }

    public override void OnUpgradeClick()
    {
        base.OnUpgradeClick();
        SaleValue = mUpgradeCost / 2;
    }
}
