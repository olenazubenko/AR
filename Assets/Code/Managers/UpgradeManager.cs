using System;
using DataModels;
using Utils;
using Zenject;

namespace Managers
{
    public class UpgradeManager : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        [Inject] private LoadDataManager _loadDataManager;
        [Inject] private PlayerManager _playerManager;
        
        private Upgrades[] _armor;
        private Upgrades[] _range;
        private Upgrades[] _reload;
        private Upgrades[] _power;
        
        public UpgradeManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RedyGameParts>(GetUpgrades);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<RedyGameParts>(GetUpgrades);
        }

        private void GetUpgrades(RedyGameParts args)
        {
            if (args.GameParts == GameParts.Data)
            {
                foreach (var t in _loadDataManager.DataModel.upgrades)
                {
                    switch (t.type)
                    {
                        case UpgradesType.CountLives:
                            _armor = t.upgradesLevel;
                            break;
                        case UpgradesType.ShootPower:
                            _power = t.upgradesLevel;
                            break;
                        case UpgradesType.ReloadSpeed:
                            _reload = t.upgradesLevel;
                            break;
                        case UpgradesType.SootRange:
                            _range = t.upgradesLevel;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        public float TryUpgrade(UpgradesType point, int upgradeLevel)
        {
            float needMoney = 0;
            switch (point)
            {
                case UpgradesType.CountLives:
                    needMoney = _armor[upgradeLevel].gold;
                    if (_playerManager.PlayerModel.PlayerMoney >= needMoney)
                    {
                        _playerManager.PlayerModel.PlayerMoney -= needMoney;
                        _playerManager.PlayerModel.PlayerArmor++;
                        return 0;
                    }
                    break;
                case UpgradesType.ShootPower:
                    needMoney = _power[upgradeLevel].gold;
                    if (_playerManager.PlayerModel.PlayerMoney >= needMoney)
                    {
                        _playerManager.PlayerModel.PlayerMoney -= needMoney;
                        _playerManager.PlayerModel.PlayerPower++;
                        return 0;
                    }
                    break;
                case UpgradesType.ReloadSpeed:
                    needMoney = _reload[upgradeLevel].gold;
                    if (_playerManager.PlayerModel.PlayerMoney >= needMoney)
                    {
                        _playerManager.PlayerModel.PlayerMoney -= needMoney;
                        _playerManager.PlayerModel.PlayerReload++;
                        return 0;
                    }
                    break;
                case UpgradesType.SootRange:
                    needMoney = _range[upgradeLevel].gold;
                    if (_playerManager.PlayerModel.PlayerMoney >= needMoney)
                    {
                        _playerManager.PlayerModel.PlayerMoney -= needMoney;
                        _playerManager.PlayerModel.PlayerRange++;
                        return 0;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return -1*(_playerManager.PlayerModel.PlayerMoney - needMoney);
        }
    }
}