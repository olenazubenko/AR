using System;
using System.Collections;
using System.Collections.Generic;
using Code.Bullet;
using Code.Controllers.SimpleControllers.PersonageControllers;
using Code.Pulls;
using UnityEngine;
using Zenject;

public class EnemyMainController : MainController
{
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private EnemyСaptureController _enemyСaptureController;
    
    public EnemyController EnemyController => _enemyController;
    public EnemyСaptureController EnemyСaptureController => _enemyСaptureController;
    

    
    [Inject] private PlayerMainController _playerMainController;
    public PlayerMainController PlayerMainController => _playerMainController;

    private void Start()
    {
        base.Start();
        EnemyController.Init(this);
        EnemyСaptureController.Init(this);
    }

    private void Update()
    {
        base.Update();
        EnemyController.Update();
    }
    
    protected void FixedUpdate()
    {
        EnemyСaptureController.Update();
    }
    
}
