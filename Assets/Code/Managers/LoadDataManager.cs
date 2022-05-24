using System;
using Core;
using DataModels;
using Utils;
using Zenject;

namespace Managers
{
    public class LoadDataManager: IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        
        public LoadDataManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public GameDataModel DataModel { get; private set; } 
        
        public void Initialize()
        {
            DataModel = new DatabaseLoader().ReturnData<GameDataModel>(Configs.gameData)[0];
           
            _signalBus.Fire(new RedyGameParts() { GameParts = GameParts.Data });
        }
        
        public void Dispose()
        {
            
        }
    }
}