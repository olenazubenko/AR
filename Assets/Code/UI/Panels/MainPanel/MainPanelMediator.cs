using System;
using Code.Windows;
using Code.Windows.StartWindow;
using DataModels;
using Managers;
using UnityEngine;
using Zenject;

namespace Code.Panels
{
    public class MainPanelMediator :BasePanelMediator <MainUiPanelView, MainPanelData>
    {

        private Upgrades[] _armor;
        private Upgrades[] _range;
        private Upgrades[] _reload;
        private Upgrades[] _power;
        
        protected override void ShowStart()
        {
            base.ShowStart();
            foreach (var t in ManagerContext.LoadDataManager.DataModel.upgrades)
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
            
            ChangeArmor(Data._Armor);
            ChangeLife(Data._Life);
            ChangeMoney(Data._Money);
            ChangePower(Data._Power);
            ChangeRange(Data._Range);
            ChangeExpereance(Data._XP);
            ChangeReload(Data._Reload);
            ChangeEnemy(Data._Enemy);

            ManagerContext.PlayerManager.PlayerModel.PlayerLiveHanger += ChangeLife;
            ManagerContext.PlayerManager.PlayerModel.PlayerExpereanceHanger += ChangeExpereance;
            ManagerContext.PlayerManager.PlayerModel.PlayerMoneyHanger += ChangeMoney;
            
            ManagerContext.PlayerManager.PlayerModel.PlayerReloadHanger += ChangeReload;
            ManagerContext.PlayerManager.PlayerModel.PlayerRangeHanger += ChangeRange;
            ManagerContext.PlayerManager.PlayerModel.PlayerPowerHanger += ChangePower;
            ManagerContext.PlayerManager.PlayerModel.PlayerArmorHanger += ChangeArmor;

            LevelController.EnemyesCountHandler += ChangeEnemy;
            PlayerMainController.ArmorCountHandler += ChangeArmor;
        }
        private void ChangeLife(int life)
        {
            Target.Life = life.ToString();
        }
        
        private void ChangeExpereance(double exp)
        {
            Target.XP = exp.ToString();
        }
        
        private void ChangeMoney(float gold)
        {
            Target.Money = gold.ToString();
        }
        
        private void ChangeReload(int point)
        {
            Target.Reload = _reload[point].count.ToString()+"s";
        }
        
        private void ChangeRange(int point)
        {
            Target.Range = _range[point].count.ToString()+"m";
        }
        
        private void ChangePower(int point)
        {
            Target.Power = _power[point].count.ToString();
        }
        
        private void ChangeArmor(int point)
        {
            
            Target.Armor = _armor[point].count.ToString();
        }
        
        private void ChangeArmor(float point)
        {
            
            Target.Armor = point.ToString();
        }
        
        private void ChangeEnemy(int point)
        {
            Target.Enemy = point.ToString();
        }
    }
}