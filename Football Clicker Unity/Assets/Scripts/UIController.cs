using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject ClubRoomMenu;
    public GameObject StaffMenu;
    public GameObject SquadMenu;
    public Canvas GameMenu;
    public Canvas MainGame;

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
        ToggleMainGame(false, 0);
        ToggleGameMenu(true);
    }

    public void ShowMainGame()
    {
        ToggleMainGame(true, 1);
        ToggleGameMenu(false);
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
