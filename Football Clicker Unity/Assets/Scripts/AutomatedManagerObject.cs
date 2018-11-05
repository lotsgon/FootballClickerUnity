using UnityEngine;

public class AutomatedManagerObject : MonoBehaviour {

    public UnityEngine.UI.Text PurchaseText;

    [SerializeField]
    protected int mUnlockLevel;
    protected bool mIsEnabled = true;
    [SerializeField]
    protected UpgradeableClickerObject mObjectToManage;
    [SerializeField]
    protected Club mClub;
    [SerializeField]
    protected float mPurchaseCost;

    // Use this for initialization
    public virtual void Start () {
        mPurchaseCost = mObjectToManage.UpgradeCost * 5.15f;
	}

    // Update is called once per frame
    public virtual void Update()
    {
        UpdateText();
    }

    // Update is called once per second
    public virtual void FixedUpdate () {
		if(!mIsEnabled && mObjectToManage.TimeUntilIncome <= 0)
        {
            mObjectToManage.OnIncomeClick();
        }
	}

    public virtual void UpdateText()
    {
        PurchaseText.text = $"Purchase: {mPurchaseCost}";
    }

    public void PurchaseClick()
    {
        if (mIsEnabled && mObjectToManage.UpgradeLevel > 0)
        {
            mIsEnabled = false;
            mClub.UpdateMoney(-mPurchaseCost);
        }
    }
}
