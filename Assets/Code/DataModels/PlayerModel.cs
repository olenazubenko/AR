using System;
using Code.Windows;

namespace DataModels
{
    [System.Serializable]
    public class PlayerModel
    {
        public string PlayerName = "PLAYER";
        
        /// <summary>
        /// 
        /// </summary>
        private int _playerLevel = 0;
        private readonly OneListener<int> _playerLevelListener = new OneListener<int>();
        public event Action<int> PlayerLevelHanger
        {
            add { _playerLevelListener.Add(value); }
            remove { _playerLevelListener.Remove(value); }
        }
        public int PlayerLevel
        {
            get => _playerLevel;
            set
            {
                _playerLevel = value;
                _playerLevelListener.SafeInvoke(_playerLevel);
            }
        } 
        
        /// <summary>
        /// 
        /// </summary>
        private int _playerLive = 0;
        private readonly OneListener<int> _playerLiveListener = new OneListener<int>();
        public event Action<int> PlayerLiveHanger
        {
            add { _playerLiveListener.Add(value); }
            remove { _playerLiveListener.Remove(value); }
        }
        public int PlayerLive
        {
            get => _playerLive;
            set
            {
                _playerLive = value;
                _playerLiveListener.SafeInvoke(_playerLive);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        private float _playerMoney = 0;
        private readonly OneListener<float> _playerMoneyListener = new OneListener<float>();
        public event Action<float> PlayerMoneyHanger
        {
            add { _playerMoneyListener.Add(value); }
            remove { _playerMoneyListener.Remove(value); }
        }
        public float PlayerMoney
        {
            get => _playerMoney;
            set
            {
                _playerMoney = value;
                _playerMoneyListener.SafeInvoke(_playerMoney);
            }
        } 
        
        private double _playerExpereance = 0;
        private readonly OneListener<double> _playerExpereanceListener = new OneListener<double>();
        public event Action<double> PlayerExpereanceHanger
        {
            add { _playerExpereanceListener.Add(value); }
            remove { _playerExpereanceListener.Remove(value); }
        }
        public double PlayerExpereance
        {
            get => _playerExpereance;
            set
            {
                _playerExpereance = value;
                _playerExpereanceListener.SafeInvoke(_playerExpereance);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        private int _playerReload = 0;
        private readonly OneListener<int> _playerReloadListener = new OneListener<int>();
        public event Action<int> PlayerReloadHanger
        {
            add { _playerReloadListener.Add(value); }
            remove { _playerReloadListener.Remove(value); }
        }
        public int PlayerReload
        {
            get => _playerReload;
            set
            {
                _playerReload = value;
                _playerReloadListener.SafeInvoke(_playerReload);
            }
        } 
        
        /// <summary>
        /// 
        /// </summary>
        private int _playerArmor = 0;
        private readonly OneListener<int> _playerArmorListener = new OneListener<int>();
        public event Action<int> PlayerArmorHanger
        {
            add { _playerArmorListener.Add(value); }
            remove { _playerArmorListener.Remove(value); }
        }
        public int PlayerArmor
        {
            get => _playerArmor;
            set
            {
                _playerArmor = value;
                _playerArmorListener.SafeInvoke(_playerArmor);
            }
        } 
        
        /// <summary>
        /// 
        /// </summary>
        private int _playerPower = 0;
        private readonly OneListener<int> _playerPowerListener = new OneListener<int>();
        public event Action<int> PlayerPowerHanger
        {
            add { _playerPowerListener.Add(value); }
            remove { _playerPowerListener.Remove(value); }
        }
        public int PlayerPower
        {
            get => _playerPower;
            set
            {
                _playerPower = value;
                _playerPowerListener.SafeInvoke(_playerPower);
            }
        } 
        
        /// <summary>
        /// 
        /// </summary>
        private int _playerRange = 0;
        private readonly OneListener<int> _playerRangeListener = new OneListener<int>();
        public event Action<int> PlayerRangeHanger
        {
            add { _playerRangeListener.Add(value); }
            remove { _playerRangeListener.Remove(value); }
        }
        public int PlayerRange
        {
            get => _playerRange;
            set
            {
                _playerRange = value;
                _playerRangeListener.SafeInvoke(_playerRange);
            }
        }

        public int _time = 0;
    }
}

