using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;
using UnityEngine.Analytics;

[RequireComponent(typeof(Button))]
public class UnityAdsDoubleIncomeButton : MonoBehaviour
{
    [SerializeField]
    private bool testMode = true;
    [SerializeField]
    private string placementId = "rewardedVideo";
    private Button adButton;

    [SerializeField]
    private Club mClub;
    [SerializeField]
    private UIController uIController;

#if UNITY_IOS
   private string gameId = "3056781";
#elif UNITY_ANDROID
    private string gameId = "3056780";
#endif

    void Start()
    {
        adButton = GetComponent<Button>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowAd);
        }

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, testMode);
        }
    }

    void Update()
    {
        if (adButton)
        {
            adButton.interactable = Monetization.IsReady(placementId);
        }
    }

    void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
        AnalyticsEvent.AdStart(true);
        AnalyticsEvent.Custom("DoubleIncomeAdvert-Pink-2");
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            mClub.UpdateMoney(mClub.IncomeWhileAway);
            AnalyticsEvent.AdComplete(true);
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
            AnalyticsEvent.AdSkip(true);
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }

        uIController.ContinueToMainGame();
    }
}