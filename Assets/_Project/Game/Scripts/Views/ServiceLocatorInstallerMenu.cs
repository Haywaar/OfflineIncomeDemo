using _Project.Features.PlayerWallet.Scripts.Domain;
using _Project.Features.PlayerWallet.Scripts.Infrastructure;
using _Project.Game.Scripts.Domain;
using UnityEngine;

namespace _Project.Game.Scripts.Views
{
    public class ServiceLocatorInstallerMenu : MonoBehaviour
    {
        private void Awake()
        {
            var playerwalletRepository = new PlayerWalletRepository();
            var playerWalletService = new PlayerWalletService(playerwalletRepository);
            playerWalletService.Initialize();
            
            ServiceLocator.Initialize();

            ServiceLocator.Current.Register(playerWalletService);
        }
    }
}