using Assets.Scripts;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class UIController : MonoBehaviour
{

    public GameObject ClubRoomMenu;
    public GameObject StaffMenu;
    public GameObject SquadMenu;
    public Canvas GameMenu;
    public Canvas MainGame;
    public Canvas IAPMenu;
    public Canvas WelcomeBackScreen;

    public CanvasGroup ClubRoomCanvasGroup;
    public CanvasGroup StaffCanvasGroup;
    public CanvasGroup SquadCanvasGroup;
    public CanvasGroup MainGameCanvasGroup;

    [SerializeField]
    private UnityEngine.UI.Text WelcomeBackIncomeText;
    [SerializeField]
    private UnityEngine.UI.Text WelcomeBackDoubleIncomeText;

    // Use this for initialization
    void Awake()
    {
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowGameMenu()
    {
        AnalyticsEvent.ScreenVisit(ScreenName.MainMenu);
        ToggleMainGame(false, 0);
        ToggleGameMenu(true);
        ToggleIAPMenu(false);
        ToggleWelcomeBackScreen(false);
    }

    public void ShowWelcomeBackScreen(Club club)
    {
        AnalyticsEvent.ScreenVisit("WelcomeBack");
        ToggleMainGame(false, 0);
        ToggleGameMenu(false);
        ToggleIAPMenu(false);
        ToggleWelcomeBackScreen(true, club);
    }

    public void ShowMainGame()
    {
        ToggleMainGame(true, 1);
        ToggleIAPMenu(false);
        ToggleGameMenu(false);
        ToggleWelcomeBackScreen(false);
    }

    public void ContinueToMainGame(Club club)
    {
        club.UpdateMoney(club.IncomeWhileAway);
        ContinueToMainGame();
    }

    public void ContinueToMainGame()
    {
        ShowSquadMenu();
        Time.timeScale = 1.0f;
    }

    public void ShowIAPMenu()
    {
        AnalyticsEvent.StoreOpened(StoreType.Premium);
        ToggleMainGame(false, 0);
        ToggleIAPMenu(true);
        ToggleGameMenu(false);
        ToggleWelcomeBackScreen(false);
    }

    public void ShowSquadMenu()
    {
        ToggleClubRoomMenu(false, 0);
        ToggleStaffMenu(false, 0);
        ToggleSquadMenu(true, 1);
        ShowMainGame();
    }

    public void ShowStaffMenu()
    {
        ToggleClubRoomMenu(false, 0);
        ToggleSquadMenu(false, 0);
        ToggleStaffMenu(true, 1);
        ShowMainGame();
    }

    public void ShowClubRoomMenu()
    {
        ToggleSquadMenu(false, 0);
        ToggleStaffMenu(false, 0);
        ToggleClubRoomMenu(true, 1);
        ShowMainGame();
    }

    private void ToggleGameMenu(bool boolean)
    {
        GameMenu.gameObject.SetActive(boolean);
    }

    private void ToggleWelcomeBackScreen(bool boolean)
    {
        WelcomeBackScreen.gameObject.SetActive(boolean);
    }

    private void ToggleWelcomeBackScreen(bool boolean, Club club)
    {
        WelcomeBackIncomeText.text = CurrencyResources.CurrencyToString(club.IncomeWhileAway);
        WelcomeBackDoubleIncomeText.text = CurrencyResources.CurrencyToString(club.IncomeWhileAway*2);
        Time.timeScale = 0.0f;
        WelcomeBackScreen.gameObject.SetActive(boolean);
    }

    private void ToggleIAPMenu(bool boolean)
    {
        IAPMenu.gameObject.SetActive(boolean);
    }

    private void ToggleMainGame(bool boolean, int alpha)
    {
        MainGameCanvasGroup.interactable = boolean;
        MainGameCanvasGroup.alpha = alpha;
        MainGameCanvasGroup.blocksRaycasts = boolean;
    }

    private void ToggleSquadMenu(bool boolean, int alpha)
    {
        SquadCanvasGroup.interactable = boolean;
        SquadCanvasGroup.alpha = alpha;
        SquadCanvasGroup.blocksRaycasts = boolean;
    }

    private void ToggleClubRoomMenu(bool boolean, int alpha)
    {
        ClubRoomCanvasGroup.interactable = boolean;
        ClubRoomCanvasGroup.alpha = alpha;
        ClubRoomCanvasGroup.blocksRaycasts = boolean;
    }

    private void ToggleStaffMenu(bool boolean, int alpha)
    {
        StaffCanvasGroup.interactable = boolean;
        StaffCanvasGroup.alpha = alpha;
        StaffCanvasGroup.blocksRaycasts = boolean;
    }

}
