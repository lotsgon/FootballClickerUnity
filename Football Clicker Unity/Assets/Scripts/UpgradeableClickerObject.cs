using UnityEngine;

public class UpgradeableClickerObject : MonoBehaviour {

    public int UpgradeLevel { get; private set; } = 0;
    public bool IsEnabled { get; private set; } = false;
    public float UpgradeCost { get { return mUpgradeCost; } private set { mUpgradeCost = value; } }
    public float TimeUntilIncome { get; private set; } = 0;
    public bool CanUpgrade { get; private set; } = true;
    public float Income { get; private set; } = 0f;
    public UnityEngine.UI.Text Info;

    [SerializeField]
    private float mUpgradeCost = 0f;
    [SerializeField]
    private Club mClub;
    private float mNewCost = 0f;

    public UpgradeableClickerObject(float upgradeCost, Club club)
    {
        club = mClub;
        upgradeCost = mUpgradeCost;
        Income = Mathf.Round(mUpgradeCost * 1.15f);
        TimeUntilIncome = Mathf.Round(mUpgradeCost * 0.03f);
    }

	// Use this for initialization
	void Start () {
        Income = Mathf.Round(mUpgradeCost * 0.05f);
    }

    // Update is called once per frame

    void Update()
    {
        Info.text = $"Cost:{UpgradeCost} Time Until: {TimeUntilIncome}";   
    }

    // Update is called once per second
    void FixedUpdate () {

        if (TimeUntilIncome > 0)
        {
            TimeUntilIncome -= 1f;
            //mClub.SetMoney(Income);
            //TimeUntilIncome = Mathf.Round(mUpgradeCost * 0.03f);
        }
	}

    public void OnClick()
    {
        if (TimeUntilIncome <= 0)
        {
            mClub.SetMoney(-mUpgradeCost);
            mUpgradeCost = Mathf.Round(mUpgradeCost * 1.15f);
            mNewCost = Mathf.Pow(mUpgradeCost, mNewCost = mUpgradeCost);
            Income = Mathf.Round(mUpgradeCost * 0.05f);
            TimeUntilIncome = Mathf.Round(mUpgradeCost * 0.03f);
        }
    }
}
