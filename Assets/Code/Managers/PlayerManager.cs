using System;
using Code.Panels;
using Code.Windows;
using DataModels;
using Newtonsoft.Json;
using UnityEngine;
using Utils;
using Zenject;

namespace Managers
{
    public class PlayerManager: IInitializable,  IDisposable
    {
        readonly SignalBus _signalBus;
        
        [Inject] private PlayerMainController _playerMainController;
        [Inject] private GameManager _gameManager;
        [Inject] private UiManagers _uiManagers;

        private string USER_DATA_SAVE_KEY = "PlayerData";
        private PlayerModel _playerModel;

        private bool _resetplayer = false;
        public PlayerModel PlayerModel
        {
            get => _playerModel;
            set => _playerModel = value;
        } 
        
        public PlayerManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _playerModel = Load<PlayerModel>();
            GameManager.StatesChanged += StatesChanged;
            if (_playerModel.PlayerExpereance == 0 )
            {
                _resetplayer = true;
            }
            _signalBus.Fire(new RedyGameParts() { GameParts = GameParts.Player });
        }
        

        public void Save()
        {
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            
            _playerModel._time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            
            PlayerPrefs.SetString(USER_DATA_SAVE_KEY, JsonConvert.SerializeObject(_playerModel));
            PlayerPrefs.Save ();
        }
        
        private T Load<T>() where T : new()
        {
            return PlayerPrefs.HasKey(USER_DATA_SAVE_KEY) ? JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(USER_DATA_SAVE_KEY)) : new T();
        }
        
        private void StatesChanged(GameStates states)
        {
            switch (states)
            {
                case GameStates.Playing:
                    Playing();
                    break;
                case GameStates.GameOver:
                    _uiManagers.Open(WindowType.GameOverWindow);
                    break;
                case GameStates.LevelLose:
                    LevelLose();
                    break;
                case GameStates.LevelWin:
                    LevelWin();
                    break;
                case GameStates.WaitingToStart:
                    WaitingToStart();
                    break;
            }
        }
        
        private void LevelWin()
        {
            _playerMainController.PersonageIsDieListener -= PlayerIsDie;
            _playerMainController.StopGame();
            PlayerModel.PlayerLevel++;
            
            _uiManagers.Open(WindowType.LevelWinWindow, GetPlayerData);
        }

        private void Playing()
        {
            _playerMainController.StartNewGame(PlayerModel);
            _playerMainController.PersonageIsDieListener += PlayerIsDie;
        }

        private void LevelLose()
        {
            _playerMainController.PersonageIsDieListener -= PlayerIsDie;
            _playerMainController.StopGame();
            PlayerModel.PlayerLive--;
            if (PlayerModel.PlayerLive == 0)
            {
                _gameManager.ShangeState(GameStates.GameOver);
                _resetplayer = true;
            }
            else
            {
                _uiManagers.Open(WindowType.LevelLoseWindow, GetPlayerData);
            }
        }

        private void WaitingToStart()
        {
            _playerMainController.StopGame();
            if (_resetplayer)
            {
                CreateNewPlayer();
                _resetplayer = false;
            }
            
            
            var panelData = new MainPanelData {
                _Money = PlayerModel.PlayerMoney,
                _Enemy = 0,
                _Life = PlayerModel.PlayerLive,
                _XP = PlayerModel.PlayerExpereance,
                _Reload = PlayerModel.PlayerReload,
                _Armor = PlayerModel.PlayerArmor,
                _Power = PlayerModel.PlayerPower,
                _Range = PlayerModel.PlayerRange
                        
            };
            _uiManagers.OpenPanel(PanelType.MainPanel, panelData);
                    
            
            _uiManagers.Open(WindowType.StartWindow, GetPlayerData);
        }

        private void PlayerIsDie(MainController playerMainController)
        {
            _gameManager.ShangeState(GameStates.LevelLose);
        }

        public void CreateNewPlayer()
        {
            PlayerModel.PlayerLive = 3;
            
            PlayerModel.PlayerLevel = 0;
            
            PlayerModel.PlayerMoney = 0;
            PlayerModel.PlayerExpereance = 0;
            
            PlayerModel.PlayerArmor = 0;
            PlayerModel.PlayerPower = 0;
            PlayerModel.PlayerRange = 0;
            PlayerModel.PlayerReload = 0;
        }

        public PlayerData GetPlayerData =>
            new PlayerData {
                _level = PlayerModel.PlayerLevel+1,
                _reload = PlayerModel.PlayerReload,
                _armor = PlayerModel.PlayerArmor,
                _power = PlayerModel.PlayerPower,
                _range = PlayerModel.PlayerRange
            };

        public void Dispose()
        {
            Save();
            GameManager.StatesChanged -= StatesChanged;
        }
    }
}