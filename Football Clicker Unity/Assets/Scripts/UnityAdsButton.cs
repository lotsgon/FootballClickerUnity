using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Monetization;

public class UnityAdsButton : MonoBehaviour
{
    [SerializeField]
    private bool testMode = true;
    [SerializeField]
    private string placementId = "video";

    [SerializeField]
    private Club mClub;

#if UNITY_IOS
   private string gameId = "3056781";
#elif UNITY_ANDROID
    private string gameId = "3056780";
#endif

    void Start()
    {
        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, testMode);
        }
    }

    void Update()
    {
        if (mClub.TimeUntilAdvert == 0)
        {
            ShowAd();
        }
    }

    void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
        AnalyticsEvent.AdStart(true);
        AnalyticsEvent.Custom("ForcedAdvert-Green-3");
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
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
        mClub.UpdateTimeUntilAdvert(300);
    }
}