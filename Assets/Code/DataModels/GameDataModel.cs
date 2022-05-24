using System.Collections.Generic;

namespace DataModels
{
    [System.Serializable]
    public class GameDataModel
    {
        public UpgradeModelData[] upgrades;
        public int playerLive;
        public int xpForKill;
        public int goldForKill;
    }

    /////////////
    
    [System.Serializable]
    public class UpgradeModelData
    {
        public Upgrades[] upgradesLevel;
        public UpgradesType type;
    }
    
    [System.Serializable]
    public class Upgrades
    {
        public float count;
        public float gold;
    }
    
    public enum UpgradesType
    {
       ShootPower,
       ReloadSpeed,
       SootRange,
       CountLives
    }

}