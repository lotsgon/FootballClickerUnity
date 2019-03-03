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

    private CanvasGroup ClubRoomCanvasGroup;
    private CanvasGroup StaffCanvasGroup;
    private CanvasGroup SquadCanvasGroup;
    private CanvasGroup MainGameCanvasGroup;

    // Use this for initialization
    void Start()
    {
        ClubRoomCanvasGroup = ClubRoomMenu.GetComponent<CanvasGroup>();
        StaffCanvasGroup = StaffMenu.GetComponent<CanvasGroup>();
        SquadCanvasGroup = SquadMenu.GetComponent<CanvasGroup>();
        MainGameCanvasGroup = MainGame.GetComponent<CanvasGroup>();

        ShowSquadMenu();
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
    }

    public void ShowMainGame()
    {
        AnalyticsEvent.ScreenVisit("MainGame");
        ToggleMainGame(true, 1);
        ToggleIAPMenu(false);
        ToggleGameMenu(false);
    }

    public void ShowIAPMenu()
    {
        AnalyticsEvent.StoreOpened(StoreType.Premium);
        ToggleMainGame(false, 0);
        ToggleIAPMenu(true);
        ToggleGameMenu(false);
    }

    public void ShowSquadMenu()
    {
        AnalyticsEvent.ScreenVisit("SquadMenu");
        ToggleClubRoomMenu(false, 0);
        ToggleStaffMenu(false, 0);
        ToggleSquadMenu(true, 1);
        ShowMainGame();
    }

    public void ShowStaffMenu()
    {
        AnalyticsEvent.ScreenVisit("StaffMenu");
        ToggleClubRoomMenu(false, 0);
        ToggleSquadMenu(false, 0);
        ToggleStaffMenu(true, 1);
        ShowMainGame();
    }

    public void ShowClubRoomMenu()
    {
        AnalyticsEvent.ScreenVisit("UpgradeMenu");
        ToggleSquadMenu(false, 0);
        ToggleStaffMenu(false, 0);
        ToggleClubRoomMenu(true, 1);
        ShowMainGame();
    }

    private void ToggleGameMenu(bool boolean)
    {
        GameMenu.gameObject.SetActive(boolean);
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
