using System;
using _Project.Features.PlayerWallet.Scripts.Domain;
using _Project.Game.Scripts.Domain;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Features.OfflineIncome.Scripts.Views
{
    public class OfflineIncomeManager : MonoBehaviour
    {
        [SerializeField] private long _offlineIncomeMinTime;
        [SerializeField] private long _offlineIncomeMaxTime;

        [SerializeField] private float _coinsPerSecond;

        [SerializeField] private GameObject _offlineIncomePopup;
        [SerializeField] private TextMeshProUGUI _offlineIncomeText;
        [SerializeField] private Button _closePopupButton;


        private const string OfflineIncomeSaveKey = "OfflineIncome_LastActiveTime";
        private float _coinsToReward;

        private void Awake()
        {
            _closePopupButton.onClick.AddListener(ClosePopup);
            StartActiveTimer();
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
            var offlineTime = GetOfflineTimeInSeconds();
            Debug.Log("offline time "   + offlineTime);
            var needToShowPopup = offlineTime > _offlineIncomeMinTime;
            if (needToShowPopup)
            {
                var coinsToReward = offlineTime * _coinsPerSecond;
                ShowOfflineIncomePopup(offlineTime, coinsToReward);
            }
        }

        private long GetOfflineTimeInSeconds()
        {
            var previousTimeStr = PlayerPrefs.GetString(OfflineIncomeSaveKey);
            
            if(long.TryParse(previousTimeStr, out var previousTime));
            var currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            var delta = currentTime - previousTime;
            return Math.Min(delta, _offlineIncomeMaxTime);
        }
        
        private void StartActiveTimer()
        {
            Observable.Interval(TimeSpan.FromSeconds(1.0f))
                .Subscribe(_ => SaveTime())
                .AddTo(this);
        }

        private void SaveTime()
        {
            var currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            PlayerPrefs.SetString(OfflineIncomeSaveKey, currentTime.ToString());
            Debug.Log("Save " + currentTime );
        }

        private void ShowOfflineIncomePopup(long offlineTime, float coinsReward)
        {
            _coinsToReward = coinsReward;
            _offlineIncomePopup.SetActive(true);
            _offlineIncomeText.text = $"Тебя не было {offlineTime} секунд, вот твои {coinsReward} монеток!";
        }
        
        private void ClosePopup()
        {
            var playerWalletService = ServiceLocator.Current.Get<PlayerWalletService>();
            playerWalletService.AddCurrency(CurrencyType.Gold, _coinsToReward);
            _coinsToReward = 0;
         
            _offlineIncomePopup.SetActive(false);
        }
    }
}