using System;
using Code.Pulls;
using UnityEngine;
using Zenject;

namespace Code.Bullet
{
    public enum EnemyStrategy
    {
        Simple,
        Move
    }
    
    [System.Serializable]
    public class EnemyController:BaseController
    {
        private GameObject  _movePointer;
        public GameObject MovePointer
        {
            get => _movePointer;
            set => _movePointer = value;
        } 
        
        private EnemyStrategy  _enemyStrategy;
        public EnemyStrategy EnemyStrategy
        {
            get => _enemyStrategy;
            set => _enemyStrategy = value;
        } 
        
        private EnemyMainController _mainController;
        
        public EnemyMainController MainController => _mainController;
        
        public  virtual void Init(EnemyMainController mainController)
        {
            _mainController = mainController;
        }
        
        public void Update()
        {
            if (!MainController.IsActive) return;
            switch (EnemyStrategy)
            {
                case EnemyStrategy.Simple:
                    if (!MainController.EnemyСaptureController.PlayerСapture)
                    {
                        MainController.AnimationController.IsShoot = false;
                        MoveRandom();
                    }
                    else
                    {
                        Sooting();
                    }
                    break;
                case EnemyStrategy.Move:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveRandom()
        {
            if (MainController.MoveController.Agent.velocity.magnitude == 0)
            {
                var radius = 30f;
                var target = Vector3.zero + (Vector3)(radius * UnityEngine.Random.insideUnitCircle);
                var t = new Vector3(target.x, 0, target.y);
                MainController.MoveController.MoveToPoint(t);
                MovePointer.transform.position = t;
            }
        }
        
        private void Sooting()
        {
            if (MainController.ShootingController.MakeShoot)
            {
                MainController.MoveController.StopMoving();
                MainController.EnemyСaptureController.PrepearSooting();
            }
            
           
        }

        
    }
}