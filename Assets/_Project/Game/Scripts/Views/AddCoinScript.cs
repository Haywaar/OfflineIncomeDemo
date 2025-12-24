using _Project.Features.PlayerWallet.Scripts.Domain;
using _Project.Game.Scripts.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Game.Scripts.Views
{
    public class AddCoinScript : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private PlayerWalletService _playerWalletService;

        private void Start()
        {
            _playerWalletService = ServiceLocator.Current.Get<PlayerWalletService>();
            _button.onClick.AddListener(AddCoin);
        }

        private void AddCoin()
        {
            _playerWalletService.AddCurrency(CurrencyType.Gold, 1);
        }
    }
}