using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;
using UnityEngine.Analytics;

[RequireComponent(typeof(Button))]
public class UnityAdsButton : MonoBehaviour
{
    [SerializeField]
    private bool testMode = true;
    [SerializeField]
    private string placementId = "video";
    private Button adButton;

    [SerializeField]
    private Club mClub;

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
        if (adButton && mClub.TimeUntilAdvert == 0)
        {
            adButton.interactable = Monetization.IsReady(placementId);
        }
        else
        {
            adButton.interactable = false;
        }
    }

    void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
        AnalyticsEvent.AdStart(true);
        AnalyticsEvent.Custom("RewardedAdvert-Red-4");
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            mClub.UpdateTickets(3);
            mClub.UpdateTimeUntilAdvert(14400);
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
    }
}