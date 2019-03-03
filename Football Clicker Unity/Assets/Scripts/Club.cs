using Assets.Scripts;
using UnityEngine;

public class Club : MonoBehaviour {

    public string Name { get; private set; }
    public string LongName { get; private set; }
    public string SixLetterName { get; private set; }
    public string Nation { get; private set; }
    public string City { get; private set; }
    public int YearFounded { get; private set; }
    public Vector3 TeamColour { get; private set; }
    public float Money { get; private set; } = 100000f;
    public int Tickets { get; private set; } = 5;
    public long Value { get; private set; }

    public UnityEngine.UI.Text MoneyDisplay;
    public UnityEngine.UI.Text TicketsDisplay;

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
}
