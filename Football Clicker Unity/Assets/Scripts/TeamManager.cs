using System.Collections.Generic;

using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public UnityEngine.UI.Text PurchaseText;

    [SerializeField]
    protected int mUnlockLevel;
    public bool mFullSquad = false;
    protected bool mIsEnabled = true;
    [SerializeField]
    protected Club mClub;
    [SerializeField]
    protected float mPurchaseCost;

    private IEnumerable<SquadPlayer> mSquadPlayers;

    // Use this for initialization
    public void Start()
    {
        mSquadPlayers = FindObjectsOfType<SquadPlayer>();
    }

    // Update is called once per second
    public void FixedUpdate()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        CheckFullSquad();
        UpdateText();
    }

    public virtual void UpdateText()
    {
        PurchaseText.text = $"Purchase: {mPurchaseCost}";
    }

    public void PurchaseClick()
    {
        if (mIsEnabled)
        {
            mIsEnabled = false;
            mClub.UpdateMoney(-mPurchaseCost);
        }
    }

    private void CheckFullSquad()
    {
        if (!mFullSquad)
        {
            var i = 0;
            foreach (SquadPlayer player in mSquadPlayers)
            {
                if (player.UpgradeLevel == 0)
                {
                    i++;
                }
            }

            if (i > 0)
            {
                mFullSquad = false;
                return;
            }
        }

        mFullSquad = true;
        foreach (SquadPlayer player in mSquadPlayers)
        {
            if (player.TimeUntilIncome <= 0)
            {
                player.OnIncomeClick();
            }
        }
    }
}