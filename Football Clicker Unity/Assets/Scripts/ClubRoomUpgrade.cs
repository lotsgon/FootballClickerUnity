using System.Collections.Generic;
using UnityEngine;

public class ClubRoomUpgrade : UpgradeableClickerObject
{

    [SerializeField]
    private readonly string mItemName;
    [SerializeField]
    private readonly int mUpgradeUnlockLevel;
    private IEnumerable<SquadPlayer> mSquadPlayers;

    public ClubRoomUpgrade(float upgradeCost, Club club, float upgradeCoefficient) : base(upgradeCost, club, upgradeCoefficient)
    {
        mSquadPlayers = FindObjectsOfType<SquadPlayer>();
    }

    // Use this for initialization
    public override void Start()
    {
        mSquadPlayers = FindObjectsOfType<SquadPlayer>();
        Application.targetFrameRate = 30;
        base.Start();
    }

    public override void OnUpgradeClick()
    {
        var i = 0;
        foreach (SquadPlayer player in mSquadPlayers)
        {
            if (player.UpgradeLevel == 0)
            {
                i++;
            }
        }

        if(i > 0)
        {
            base.IsEnabled = false;
            return;
        }

        base.IsEnabled = true;
        base.OnUpgradeClick();
    }

    //Update is called once per frame

    //void Update()
    //{

    //}
}
