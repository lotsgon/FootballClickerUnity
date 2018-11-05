using UnityEngine;

public class SquadPlayer : UpgradeableClickerObject {

    public string Position { get { return mPosition; } set { mPosition = value; } }
    public float SaleValue { get; private set; }
    public UnityEngine.UI.Text SellText;

    [SerializeField]
    private string mPosition;
    [SerializeField]
    private TeamManager mTeamManager;

    public SquadPlayer(float upgradeCost, Club club, string postition) : base(upgradeCost, club)
    {
        mPosition = postition;
        SaleValue = upgradeCost / 2;
    }

    // Use this for initialization
    void Start()
    {
        base.IsEnabled = true;
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
            mUpgradeCost = mUpgradeCost * 1.05f;
            UpdateUpgradeIncome(0.0f, 0.0f);
            UpgradeLevel = 0;
            if(mTeamManager != null)
            {
                mTeamManager.mFullSquad = false;
            }
        }
    }

    public override void OnUpgradeClick()
    {
        base.OnUpgradeClick();
        SaleValue = mUpgradeCost / 2;
    }

    public override void UpdateText()
    {
        base.UpdateText();
        SellText.text = $"Sell: {SaleValue}";
    }
}
