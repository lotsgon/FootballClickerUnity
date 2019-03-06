using Assets.Scripts;
using System;
using UnityEngine;

[System.Serializable]
public class ClubData
{
    public float Money;
    public int Tickets;
    public long SaveTime;

    public ClubData(Club club)
    {
        Money = club.Money;
        Tickets = club.Tickets;
        SaveTime = club.SaveTime;
    }
}

public class Club : MonoBehaviour
{
    public string Name { get; private set; }
    public string LongName { get; private set; }
    public string SixLetterName { get; private set; }
    public string Nation { get; private set; }
    public string City { get; private set; }
    public int YearFounded { get; private set; }
    public Vector3 TeamColour { get; private set; }
    public float Money { get; private set; }
    public int Tickets { get; private set; }
    public long Value { get; private set; }
    public long SaveTime { get; private set; }

    public UnityEngine.UI.Text MoneyDisplay;
    public UnityEngine.UI.Text TicketsDisplay;

    public Club()
    {
        Money = 100000;
        Tickets = 5;
    }

    public void Awake()
    {
        SaveLoadManager.LoadClub();

        var players = UnityEngine.Object.FindObjectsOfType<SquadPlayer>();

        foreach(SquadPlayer player in players)
        {
            SaveLoadManager.LoadAutomatedManager(player.Position);
            SaveLoadManager.LoadUpgrade(player.Position);
            SaveLoadManager.LoadSquadPlayer(player.Position);
            player.CalculateMoneyEarntWhileAway();
        }
    }

    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        MoneyDisplay.text = CurrencyResources.CurrencyToString(Money);
        TicketsDisplay.text = Tickets.ToString();
    }

    // Update is called once per second
    void FixedUpdate () {

    }

    public void UpdateMoney(float money)
    {
        Money += money;
    }

    public void UpdateTickets(int tickets)
    {
        Tickets += tickets;
    }

    public void SetClubData(ClubData clubData)
    {
        Money = clubData.Money;
        Tickets = clubData.Tickets;
        SaveTime = clubData.SaveTime;
    }

    private void OnApplicationPause()
    {
        SaveTime = DateTime.Now.ToFileTime();
        SaveLoadManager.SaveClub(this);
    }

    private void OnApplicationQuit()
    {
        SaveTime = DateTime.Now.ToFileTime();
        SaveLoadManager.SaveClub(this);
    }
}
