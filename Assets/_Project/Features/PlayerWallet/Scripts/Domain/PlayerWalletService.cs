using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Features.PlayerWallet.Scripts.Infrastructure;
using _Project.Game.Scripts.Domain;
using R3;
using UnityEngine;

namespace _Project.Features.PlayerWallet.Scripts.Domain
{
    public class PlayerWalletService : IService
    {
        private readonly PlayerWalletRepository _repository;
        private Dictionary<CurrencyType, float> _playerCurrencies;

        public readonly Subject<CurrencyType> OnCurrencyChanged = new();
        
        public PlayerWalletService(PlayerWalletRepository repository)
        {
            _repository = repository;
        }

        public void Initialize()
        {
            var currencyTypes = Enum.GetValues(typeof(CurrencyType)).Cast<CurrencyType>();
            _playerCurrencies = new Dictionary<CurrencyType, float>();
            foreach (var curenncyType in currencyTypes)
            {
                var amount = _repository.GetCurrencyAmount(curenncyType);
                _playerCurrencies[curenncyType] = amount;
            }
        }

        public void AddCurrency(CurrencyType currencyType, float amount)
        {
            _playerCurrencies[currencyType] += amount;
            _repository.SaveCurrency(currencyType, GetCurrencyAmount(currencyType));
            OnCurrencyChanged.OnNext(currencyType);
        }

        public void SubstractCurrency(CurrencyType currencyType, float amount)
        {
            var currencyAmount = GetCurrencyAmount(currencyType);
            if (currencyAmount < amount)
            {
                Debug.LogError("Cannot substract currency");
                return;
            }
            
            _playerCurrencies[currencyType] -= amount;
           
            _repository.SaveCurrency(currencyType,GetCurrencyAmount(currencyType));
            
            OnCurrencyChanged.OnNext(currencyType);
        }

        public float GetCurrencyAmount(CurrencyType currencyType)
        {
            return _playerCurrencies[currencyType];
        }
    }
}
