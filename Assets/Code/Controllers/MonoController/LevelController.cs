using System;
using System.Collections.Generic;
using System.Linq;
using Code.Controllers;
using UnityEngine;

namespace Code
{
    public class LevelController : Controller
    {
        [SerializeField] private int _id;
        [SerializeField] private List<Transform> _spaunPositions;
        [SerializeField] private List<int> _enamyesID;
        
        private bool _isCurrentLevel = false;

        private List<MainController> _activeEnemy = new List<MainController>();
        public bool IsCurrentLevel {
            get => _isCurrentLevel;
            set => _isCurrentLevel = value;
        }
        public int Id => _id;
        public List<Transform> SpaunPositions => _spaunPositions;

        private readonly OneListener<LevelController> _levelEnd = new OneListener<LevelController>();
        public event Action<LevelController> LevelEnd
        {
            add { _levelEnd.Add(value); }
            remove { _levelEnd.Remove(value); }
        }
        
        private static readonly OneListener<int> _enemyesCountListener = new OneListener<int>();
        public static event Action<int> EnemyesCountHandler
        {
            add { _enemyesCountListener.Add(value); }
            remove { _enemyesCountListener.Remove(value); }
        }

        private int _enemyCounter = 0;
        
        public void LevelEnable(bool start)
        {
            _isCurrentLevel = start;
            gameObject.SetActive(start);

            if (start)
            {
                ActivateEnemyes();
            }
            else
            {
                DeactivateEnemyes();
            }
        }

        private void DeactivateEnemyes()
        {
            foreach (var t in _activeEnemy)
            {
                t.SetActivePersonage = false;
                t.PersonageIsDieListener -= EnemyIsDie;
                t.gameObject.SetActive(false);
            }
            _activeEnemy.Clear();
            _enemyCounter = 0 ;
            _enemyesCountListener.SafeInvoke(_enemyCounter);
        }

        private void ActivateEnemyes()
        {
            for (var i = 0; i < _spaunPositions.Count; i++)
            {
                var enemy = ManagerContext.InstantiateManagers.InstantiateEnemy(_spaunPositions[i].position, _enamyesID[i]);
                enemy.SetActivePersonage = true;
                enemy.PersonageIsDieListener += EnemyIsDie;
                _activeEnemy.Add(enemy);
            }

            _enemyCounter = _spaunPositions.Count;
            _enemyesCountListener.SafeInvoke(_enemyCounter);
        }

        private void EnemyIsDie(MainController enemy)
        {
            enemy.PersonageIsDieListener -= EnemyIsDie;
            _enemyCounter--;
            _enemyesCountListener.SafeInvoke(_enemyCounter);
            if (_activeEnemy.Any(t => t.IsActive == true))
            {
                return;
            }
            
            _levelEnd.SafeInvoke(this);
        }
    }
}