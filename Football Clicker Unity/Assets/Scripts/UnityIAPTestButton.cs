using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Button))]
    public class UnityIAPTestButton : MonoBehaviour
    {
        [SerializeField]
        private string storeItemId;
        [SerializeField]
        private float storeItemPrice;
        [SerializeField]
        private int amount;
        private Button iapButton;

        [SerializeField]
        private Club mClub;

        void Start()
        {
            iapButton = GetComponent<Button>();
            if (iapButton)
            {
                iapButton.onClick.AddListener(MakePurchase);
            }
        }

        void MakePurchase()
        {
            Analytics.Transaction(storeItemId, (decimal)storeItemPrice, "GBP");
            AnalyticsEvent.IAPTransaction("Tickets-Red-4", storeItemPrice, storeItemId);
            AnalyticsEvent.ItemAcquired(AcquisitionType.Premium, "Tickets-Red-4", storeItemPrice, storeItemId);
            AnalyticsEvent.StoreItemClick(StoreType.Premium, storeItemId);
            mClub.UpdateTickets(amount);
        }
    }
}
