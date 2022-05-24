using Zenject;

namespace Managers
{
    public class PurchasingManager: IInitializable
    {
        [Inject] private PlayerManager _playerManager;
        
        public PurchasingManager(SignalBus signalBus)
        {
            
        }

        public void Initialize()
        {
            
        }

        public bool TryBuyMoney(float money)
        {
            _playerManager.PlayerModel.PlayerMoney += money;
            return true;
        }
    }
}