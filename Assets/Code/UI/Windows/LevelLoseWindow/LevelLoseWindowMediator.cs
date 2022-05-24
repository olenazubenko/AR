using System;
using Code.Panels;
using DataModels;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Code.Windows.StartWindow
{
    public class LevelLoseWindowMediator :BaseWindowMediator<LevelLoseWindowView, PlayerData>
    {
        private Upgrades[] _armor;
        private Upgrades[] _range;
        private Upgrades[] _reload;
        private Upgrades[] _power;

        private float _needMoneyToUpgrade = 0;
        
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

            ChangePower(Data._power);
            ChangeRange(Data._range);
            ChangeArmor(Data._armor);
            ChangeReload(Data._reload);
            Target.NextLevel = Data._level.ToString();
            
            ManagerContext.PlayerManager.PlayerModel.PlayerReloadHanger += ChangeReload;
            ManagerContext.PlayerManager.PlayerModel.PlayerRangeHanger += ChangeRange;
            ManagerContext.PlayerManager.PlayerModel.PlayerPowerHanger += ChangePower;
            ManagerContext.PlayerManager.PlayerModel.PlayerArmorHanger += ChangeArmor;
            
            Target.StartButton.onClick.AddListener(OnStartGame);
            
            Target.LifeInLevelButton.onClick.AddListener(OnUpgradeArmor);
            Target.SootingPowerButton.onClick.AddListener(OnUpgradePower);
            Target.SootingRangeButton.onClick.AddListener(OnUpgradeRange);
            Target.ReloadSpeedButton.onClick.AddListener(OnUpgradeReload);
            
            Target.PanelStart.SetActive(true);
            Target.PanelNoMoney.SetActive(false);
        }

        private void OnStartGame()
        {
            Target.StartButton.onClick.RemoveListener(OnStartGame);
            ManagerContext.UiManagers.Close(AfterClose);
        }
        
        private void AfterClose()
        {
            ManagerContext.GameManager.ShangeState(GameStates.Playing); 
            
            Target.LifeInLevelButton.onClick.RemoveListener(OnUpgradeArmor);
            Target.SootingPowerButton.onClick.RemoveListener(OnUpgradePower);
            Target.SootingRangeButton.onClick.RemoveListener(OnUpgradeRange);
            Target.ReloadSpeedButton.onClick.RemoveListener(OnUpgradeReload);
        }
        
        private void ChangeReload(int point)
        {
            Target.ReloadSpeed = _reload[point].count.ToString()+"s";
            if (_reload.Length == point+1)
            {
                Target.ReloadSpeedButtonText = "MAX";
                Target.ReloadSpeedButton.enabled = false;
            }
            else
            {
                Target.ReloadSpeedButtonText = _reload[point+1].gold.ToString();
                Target.ReloadSpeedButton.enabled = true;
            }
        }
        
        private void ChangeRange(int point)
        {
            Target.SootingRange = _range[point].count.ToString()+"m";
            if (_range.Length == point+1)
            {
                Target.SootingRangeButtonText = "MAX";
                Target.SootingRangeButton.enabled = false;
            }
            else
            {
                Target.SootingRangeButtonText = _range[point+1].gold.ToString();
                Target.SootingRangeButton.enabled = true;
            }
        }
        
        private void ChangePower(int point)
        {
            Target.SootingPower = _power[point].count.ToString();
            if (_power.Length == point+1)
            {
                Target.SootingPowerButtonText = "MAX";
                Target.SootingPowerButton.enabled = false;
            }
            else
            {
                Target.SootingPowerButtonText = _power[point+1].gold.ToString();
                Target.SootingPowerButton.enabled = true;
            }
        }
        
        private void ChangeArmor(int point)
        {
            Target.LifeInLevel = _armor[point].count.ToString();
            if (_armor.Length == point+1)
            {
                Target.LifeInLevelButtonText = "MAX";
                Target.LifeInLevelButton.enabled = false;
            }
            else
            {
                Target.LifeInLevelButtonText = _armor[point+1].gold.ToString();
                Target.LifeInLevelButton.enabled = true;
            }
        }

        private void OnUpgradeArmor()
        {
            _needMoneyToUpgrade = ManagerContext.UpgradeManager.TryUpgrade(UpgradesType.CountLives, Data._armor+1);
            if(0 ==  _needMoneyToUpgrade)
            {
                Data._armor = ManagerContext.PlayerManager.PlayerModel.PlayerArmor;
            }
            else
            {
                HideStartPanel();
            }
            
        }
        
        private void OnUpgradePower()
        {
            _needMoneyToUpgrade = ManagerContext.UpgradeManager.TryUpgrade(UpgradesType.ShootPower, Data._power+1);
            if(0 ==  _needMoneyToUpgrade)
            {
                Data._power = ManagerContext.PlayerManager.PlayerModel.PlayerPower;
            }
            else
            {
                HideStartPanel();
            }
        }
        
        private void OnUpgradeRange()
        {
            _needMoneyToUpgrade = ManagerContext.UpgradeManager.TryUpgrade(UpgradesType.SootRange, Data._range+1);
            if(0 ==  _needMoneyToUpgrade)
            {
                Data._range = ManagerContext.PlayerManager.PlayerModel.PlayerRange;
            }
            else
            {
                HideStartPanel();
            }
        }
        
        private void OnUpgradeReload()
        {
            _needMoneyToUpgrade = ManagerContext.UpgradeManager.TryUpgrade(UpgradesType.ReloadSpeed, Data._reload+1);
            if(0 ==  _needMoneyToUpgrade)
            {
                Data._reload = ManagerContext.PlayerManager.PlayerModel.PlayerReload;
            }
            else
            {
                HideStartPanel();
            }
        }

        private void HideStartPanel()
        {
            Target.NeedMoreMoneyText = "You need by <color=green>"+_needMoneyToUpgrade.ToString()+"</color> gold";
            Target.PanelNoMoney.transform.localScale = Vector3.zero;
            Target.PanelNoMoney.SetActive(true);
            Target.PanelStart.transform.DOScale(Vector3.zero, 0.2F).OnComplete(ShowGetMoreMoney);
        }

        private void ShowGetMoreMoney()
        {
            Target.PanelStart.SetActive(false);
            
            Target.PanelNoMoney.transform.DOScale(Vector3.one, 0.2F);
            Target.GetMoreMoneyButton.onClick.AddListener(GetMoreMoney);
            Target.CancelButton.onClick.AddListener(HideGetMoreMoney);
        }
        

        private void GetMoreMoney()
        {
            ManagerContext.PurchasingManager.TryBuyMoney(_needMoneyToUpgrade);
            HideGetMoreMoney();
        }

        private void HideGetMoreMoney()
        {
            Target.PanelStart.SetActive(true);
            Target.PanelNoMoney.SetActive(false);
            Target.PanelNoMoney.transform.DOScale(Vector3.zero, 0.2F).OnComplete(ShowStartPanel);
            Target.GetMoreMoneyButton.onClick.RemoveListener(GetMoreMoney);
            Target.CancelButton.onClick.RemoveListener(HideGetMoreMoney);
            _needMoneyToUpgrade = 0;
        }

        private void ShowStartPanel()
        {
            Target.PanelStart.transform.DOScale(Vector3.one, 0.2F);
        }
        
    }
}