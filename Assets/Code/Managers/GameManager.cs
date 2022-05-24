using System;
using System.Collections.Generic;
using Code.Windows;
using Code.Windows.StartWindow;
using UnityEngine;
using Utils;
using Zenject;

namespace Managers
{
   
    public enum GameStates
    {
        WaitingToStart,
        ReadyUi,
        ReadyPlayer,
        ReadyData,
        Playing,
        LevelWin,
        LevelLose,
        GameOver
    }

    public class GameManager : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;


        private GameStates _state  = GameStates.WaitingToStart;
        public GameStates State => _state;
        private List<GameParts> _needToStart = new List<GameParts>(){GameParts.UI, GameParts.Player, GameParts.Data};

        private static readonly OneListener<GameStates> _statesChanged = new OneListener<GameStates>();
        public static event Action<GameStates> StatesChanged
        {
            add { _statesChanged.Add(value); }
            remove { _statesChanged.Remove(value); }
        }
        
        public GameManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RedyGameParts>(TryStart);
        }

        private void TryStart(RedyGameParts args)
        {
            switch (args.GameParts)
            {
                case GameParts.Player:
                    _state = GameStates.ReadyPlayer;
                    break;
                case GameParts.UI:
                    _state = GameStates.ReadyUi;
                    break;
                case GameParts.Data:
                    _state = GameStates.ReadyData;
                    break;
            }

            for (var i = 0; i < _needToStart.Count; i++)
            {
                if (_needToStart[i] == args.GameParts)
                {
                    _needToStart.RemoveAt(i);
                }
            }

            if (_needToStart.Count == 0)
            {
                ShangeState(GameStates.WaitingToStart);
            }
        }
        public void ShangeState(GameStates state)
        {
            _state = state;
            _statesChanged.SafeInvoke(_state);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<RedyGameParts>(TryStart);
        }
    }
}