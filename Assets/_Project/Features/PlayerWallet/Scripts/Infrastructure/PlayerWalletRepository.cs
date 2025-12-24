using _Project.Features.PlayerWallet.Scripts.Domain;
using UnityEngine;

namespace _Project.Features.PlayerWallet.Scripts.Infrastructure
{
    public class PlayerWalletRepository
    {
        private const string PlayerWalletRepositoryPrefix = "PlayerWallet_";

        public void SaveCurrency(CurrencyType currencyType, float amount)
        {
            PlayerPrefs.SetFloat(PlayerWalletRepositoryPrefix + currencyType.ToString(), amount);
        }

        public float GetCurrencyAmount(CurrencyType currencyType)
        {
            return PlayerPrefs.GetFloat(PlayerWalletRepositoryPrefix + currencyType.ToString());
        }
}
}