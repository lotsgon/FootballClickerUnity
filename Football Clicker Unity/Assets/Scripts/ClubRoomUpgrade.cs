using System.Collections.Generic;
using UnityEngine;

public class ClubRoomUpgrade : UpgradeableClickerObject
{

    [SerializeField]
    private string mItemName;
    [SerializeField]
    private int mUpgradeUnlockLevel;
    private IEnumerable<SquadPlayer> mSquadPlayers;

    public ClubRoomUpgrade(float upgradeCost, Club club) : base(upgradeCost, club)
    {
        mSquadPlayers = FindObjectsOfType<SquadPlayer>();
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        mSquadPlayers = FindObjectsOfType<SquadPlayer>();
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
