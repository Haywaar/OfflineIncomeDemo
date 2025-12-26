using System;
using R3;
using UnityEngine;

namespace _Project.Features.OfflineIncome.Scripts.Infrastructure
{
    public class OfflineIncomeRepository
    {
        private const string OfflineIncomeSaveKey = "OfflineIncome_LastActiveTime";

        public void StartActiveTimer(CompositeDisposable disposable)
        {
            Observable.Interval(TimeSpan.FromSeconds(1.0f))
                .Subscribe(_ => SaveCurrentTime())
                .AddTo(disposable);
        }
        
        public long GetOfflineTimeInSeconds()
        {
            var currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            var previousTime = GetPreviousOfflineTime();

            var delta = currentTime - previousTime;
            return delta;
        }
        
        private void SaveCurrentTime()
        {
            var currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            PlayerPrefs.SetString(OfflineIncomeSaveKey, currentTime.ToString());
        }
        
        private long GetPreviousOfflineTime()
        {
            var currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            var previousTime = currentTime;
            var previousTimeStr = PlayerPrefs.GetString(OfflineIncomeSaveKey);
            if (!long.TryParse(previousTimeStr, out previousTime))
            {
                Debug.LogError("Can't parse offline time, value: " + previousTimeStr);
            }

            return previousTime;
        }
    }
}