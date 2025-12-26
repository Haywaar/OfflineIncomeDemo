using _Project.Features.OfflineIncome.Scripts.Domain;
using _Project.Features.OfflineIncome.Scripts.Infrastructure;
using _Project.Features.PlayerWallet.Scripts.Domain;
using _Project.Game.Scripts.Domain;
using R3;
using UnityEngine;

namespace _Project.Features.OfflineIncome.Scripts.Views
{
    public class OfflineIncomeManager : MonoBehaviour
    {
        [SerializeField] private OfflineIncomeConfig _offlineIncomeConfig;
        [SerializeField] private OfflineIncomePopup _offlineIncomePopupPrefab;
        [SerializeField] private Transform _offlineIncomePopupContainer;

        private OfflineIncomeRepository _repository;
        private OfflineIncomeCalculator _calculator;
        
        private readonly CompositeDisposable _disposable = new();
        
        private void Awake()
        {
            _calculator =
                new OfflineIncomeCalculator(_offlineIncomeConfig);

            _repository = new OfflineIncomeRepository();
            _repository.StartActiveTimer(_disposable);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                TryToShowOfflineIncomePopup();
            }
        }

        private void TryToShowOfflineIncomePopup()
        {
            var offlineTime = _repository.GetOfflineTimeInSeconds();
            var needToShowPopup = offlineTime > _offlineIncomeConfig.MinTime;
            if (needToShowPopup)
            {
                var coinsToReward = _calculator.CalculateCoinReward(offlineTime);
                ShowOfflineIncomePopup(offlineTime, coinsToReward);
            }
        }

        private void ShowOfflineIncomePopup(long offlineTime, float coinsToReward)
        {
            var popup = Instantiate(_offlineIncomePopupPrefab, _offlineIncomePopupContainer);
            popup.Initialize(offlineTime, coinsToReward,OnPopupClosed);
        }

        private void OnPopupClosed(float coinsToReward)
        {
            var playerWallet = ServiceLocator.Current.Get<PlayerWalletService>();
            playerWallet.AddCurrency(CurrencyType.Gold, coinsToReward);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}