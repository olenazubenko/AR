using System;
using System.Collections;
using System.Collections.Generic;
using Code.Bullet;
using Code.Controllers.SimpleControllers.PersonageControllers;
using Code.Pulls;
using DataModels;
using UnityEngine;
using Zenject;

public class PlayerMainController : MainController
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerCaptureController _playerСaptureController;
    public PlayerController PlayerController => _playerController;
    public PlayerCaptureController PlayerСaptureController => _playerСaptureController;
    [Inject] private EnemyesPull _enemyesPull;
    public EnemyesPull EnemyesPull => _enemyesPull;
    
    private Upgrades[] _armor;
    private Upgrades[] _range;
    private Upgrades[] _reload;
    private Upgrades[] _power;
    
    private static readonly OneListener<float> _armorCountListener = new OneListener<float>();
    public static event Action<float> ArmorCountHandler
    {
        add { _armorCountListener.Add(value); }
        remove { _armorCountListener.Remove(value); }
    }
    
    protected void Start()
    {
        base.Start();
        PlayerController.Init(this);
        PlayerСaptureController.Init(this);
        
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
    }

    protected void Update()
    {
        if (!IsActive) return;
        base.Update();
        PlayerController.Update();
    }

    protected void FixedUpdate()
    {
        if (!IsActive) return;
        PlayerController.FixedUpdate();
        PlayerСaptureController.Update();
    }

    public void StartNewGame(PlayerModel playerModel)
    {
        LivesController.IsDie = false;
        transform.position = Vector3.zero;
        SetActivePersonage = true;
        LivesController.LivesCount = _armor[ManagerContext.PlayerManager.PlayerModel.PlayerArmor].count;
        ShootingController.ShootPower = _power[ManagerContext.PlayerManager.PlayerModel.PlayerPower].count;
        ShootingController.Frequency = _reload[ManagerContext.PlayerManager.PlayerModel.PlayerReload].count;
        PlayerСaptureController.Distance = _range[ManagerContext.PlayerManager.PlayerModel.PlayerRange].count;
        _armorCountListener.SafeInvoke(LivesController.LivesCount);
    }
    
    protected override void PersonageLifeCount(float lifes)
    {
        base.PersonageLifeCount(lifes);
        _armorCountListener.SafeInvoke(lifes);
    }

    public void StopGame()
    {
        SetActivePersonage = false;
        _armorCountListener.SafeInvoke(LivesController.LivesCount);
    }
    
}
