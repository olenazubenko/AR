using System;
using Code;
using Utils;
using Zenject;

namespace Managers
{
    public class LevelManager: IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        [Inject] private InstantiateManagers _instantiateManagers;
        [Inject] private PlayerManager _playerManager;
        [Inject] private GameManager _gameManager;
        
        private int _curentLevel = 0;
        private LevelController _curentLevelController;

        public LevelManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RedyGameParts>(TryStart);
            GameManager.StatesChanged += StatesChanged;
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<RedyGameParts>(TryStart);
            GameManager.StatesChanged -= StatesChanged;
        }
        
        private void TryStart(RedyGameParts args)
        {
            // if (args.GameParts == GameParts.Player)
            // {
            //     _curentLevel = _playerManager.PlayerModel.PlayerLevel;
            // }
        }

        private void StatesChanged(GameStates states)
        {
            switch (states)
            {
                case GameStates.Playing:
                    _curentLevel = _playerManager.PlayerModel.PlayerLevel;
                    StartLevel(_curentLevel);
                    break;
                case GameStates.GameOver:
                    _playerManager.PlayerModel.PlayerLevel = 0;
                    break;
                case GameStates.LevelLose:
                    FinishLevel();
                    break;
                case GameStates.LevelWin:
                    FinishLevel();
                    break;
            }
        }

        private void FinishLevel()
        {
            _curentLevelController.LevelEnd -= FinishLevel;
            _curentLevelController.LevelEnable(false);
        }
        
        private void StartLevel(int ID)
        {
            _curentLevelController = _instantiateManagers.InstantiateLevel(ID);
            _curentLevelController.LevelEnd += FinishLevel;
            _curentLevelController.LevelEnable(true);
        }

        private void FinishLevel(LevelController level)
        {
            _gameManager.ShangeState(GameStates.LevelWin);
        }
    }
}