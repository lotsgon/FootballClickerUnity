using Assets.Scripts;
using System;
using UnityEngine;

[System.Serializable]
public class ClubData
{
    public float Money;
    public int Tickets;
    public long SaveTime;
    public int TimeUntilAdvert;

    public ClubData(Club club)
    {
        Money = club.Money;
        Tickets = club.Tickets;
        SaveTime = club.SaveTime;
        TimeUntilAdvert = club.TimeUntilAdvert;
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
    public float IncomeWhileAway { get; private set; }
    public int TimeUntilAdvert { get; private set; }

    public UnityEngine.UI.Text MoneyDisplay;
    public UnityEngine.UI.Text TicketsDisplay;

    [SerializeField]
    private UIController uIController;

    public Club()
    {
        Money = 10000;
        Tickets = 5;
        TimeUntilAdvert = 0;
    }

    public void Awake()
    {
        SaveLoadManager.LoadClub();

        var saveTime = DateTime.FromFileTime(this.SaveTime);

        TimeSpan timeGone = DateTime.Now - saveTime;

        UpdateTimeUntilAdvert(Mathf.Min(0, -(int)timeGone.TotalSeconds));

        var players = UnityEngine.Object.FindObjectsOfType<SquadPlayer>();

        float incomeWhileAway = 0.0f;

        foreach (SquadPlayer player in players)
        {
            SaveLoadManager.LoadAutomatedManager(player.Position);
            SaveLoadManager.LoadUpgrade(player.Position);
            SaveLoadManager.LoadSquadPlayer(player.Position);
            incomeWhileAway += player.CalculateMoneyEarntWhileAway();
        }

        if (incomeWhileAway > 0)
        {
            IncomeWhileAway = incomeWhileAway;
            uIController.ShowWelcomeBackScreen(this);
        }
        else
        {
            uIController.ContinueToMainGame(this);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoneyDisplay.text = CurrencyResources.CurrencyToString(Money);
        TicketsDisplay.text = Tickets.ToString();
    }

    // Update is called once per second
    void FixedUpdate()
    {
        if (TimeUntilAdvert > 0)
        {
            TimeUntilAdvert -= 1;
        }
    }

    public void UpdateMoney(float money)
    {
        Money += money;
    }

    public void UpdateTickets(int tickets)
    {
        Tickets += tickets;
    }

    public void UpdateTimeUntilAdvert(int time)
    {
        TimeUntilAdvert = Mathf.Clamp(TimeUntilAdvert + time, 0, 14400);
    }

    public void SetClubData(ClubData clubData)
    {
        Money = clubData.Money;
        Tickets = clubData.Tickets;
        SaveTime = clubData.SaveTime;
        TimeUntilAdvert = clubData.TimeUntilAdvert;
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
