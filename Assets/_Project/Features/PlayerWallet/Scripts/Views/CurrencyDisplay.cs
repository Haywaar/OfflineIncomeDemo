using _Project.Features.PlayerWallet.Scripts.Domain;
using _Project.Game.Scripts.Domain;
using R3;
using TMPro;
using UnityEngine;

namespace _Project.Features.PlayerWallet.Scripts.Views
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private TextMeshProUGUI _currencyText;

        private PlayerWalletService _playerWalletService;
        private void Start()
        {
            _playerWalletService = ServiceLocator.Current.Get<PlayerWalletService>();
            
            _playerWalletService.OnCurrencyChanged
                .Where(x => x == _currencyType)
                .Subscribe(_ => Redraw())
                .AddTo(this);
            
            Redraw();
        }

        private void Redraw()
        {
            var currency = _playerWalletService.GetCurrencyAmount(_currencyType);
            _currencyText.text = currency.ToString();
        }
    }
}