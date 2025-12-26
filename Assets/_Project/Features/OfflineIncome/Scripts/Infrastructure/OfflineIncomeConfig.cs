using UnityEngine;

namespace _Project.Features.OfflineIncome.Scripts.Infrastructure
{
    [CreateAssetMenu(menuName = "ScriptableObjects/OfflineIncomeConfig")]
    public class OfflineIncomeConfig : ScriptableObject
    {
        [SerializeField] private long _minTime;
        [SerializeField] private long _maxTime;
        [SerializeField] private float _coinsPerSecond;

        public long MinTime => _minTime;
        public long MaxTime => _maxTime;
        public float CoinsPerSecond => _coinsPerSecond;
    }
}