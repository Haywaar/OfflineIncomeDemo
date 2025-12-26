using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Features.OfflineIncome.Scripts.Views
{
    public class OfflineIncomePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _offlineIncomeText;
        [SerializeField] private Button _closePopupButton;

        private UnityAction<float> _onCloseAction;

        private float _coinsToReward;

        private void Awake()
        {
            _closePopupButton.onClick.AddListener(ClosePopup);
        }

        public void Initialize(long offlineTime, float coinsReward, UnityAction<float> closePopupAction)
        {
            _coinsToReward = coinsReward;
            _onCloseAction = closePopupAction;
            
            _offlineIncomeText.text = $"Тебя не было {offlineTime} секунд, вот твои {coinsReward} монеток!";
        }

        private void ClosePopup()
        {
            _onCloseAction?.Invoke(_coinsToReward);
            Destroy(gameObject);
        }
    }
}