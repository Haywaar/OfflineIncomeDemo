using System;
using _Project.Features.OfflineIncome.Scripts.Infrastructure;

namespace _Project.Features.OfflineIncome.Scripts.Domain
{
    public class OfflineIncomeCalculator
    {
        private readonly long _offlineIncomeMaxTime;
        private readonly float _coinsPerSecond;

        public OfflineIncomeCalculator(OfflineIncomeConfig offlineIncomeConfig)
        {
            _offlineIncomeMaxTime = offlineIncomeConfig.MaxTime;
            _coinsPerSecond = offlineIncomeConfig.CoinsPerSecond;
        }

        public float CalculateCoinReward(long offlineTime)
        {
            offlineTime = Math.Min(_offlineIncomeMaxTime, offlineTime);
            return offlineTime * _coinsPerSecond;
        }
    }
}