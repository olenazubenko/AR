using System.Linq;
using Code;
using Code.Pulls;
using UnityEngine;
using Util;
using Zenject;
using Random = System.Random;

namespace Managers
{
    public class InstantiateManagers
    {
        [Inject] private BulletsPull _bulletsPull;
        [Inject] private EnemyesPull _enemyesPull;
        [Inject] private LevelPull _levelPull;
        [Inject] private PositionPointerPull _movePointerPull;
        
        
        [Inject] private FactoryBullet _factoryBullet;
        [Inject] private FactoryEnemy _factoryEnemy;
        [Inject] private FactoryLevel _factoryLevel;
        [Inject] private FactoryMovePointer _factoryMovePointer;
        
        [Inject] private FactoryWindow _factoryWindow;
        
        public BulletController InstantiateBullet(Transform bulletPosition)
        {
            foreach (var t in _bulletsPull.Bullets.Where(t => t.IsUsed == false))
            {
                t.gameObject.SetActive(true);
                t.gameObject.transform.position = bulletPosition.position;
                t.gameObject.transform.rotation = bulletPosition.rotation;;
                return t;
            }
            
            var bullet = _factoryBullet.Create();
            bullet.gameObject.transform.SetParent(_bulletsPull.gameObject.transform,false);
            bullet.gameObject.transform.position = bulletPosition.position;
            bullet.gameObject.transform.rotation = bulletPosition.rotation;
            _bulletsPull.Bullets.Add(bullet);
            return bullet;
        }
        
        public EnemyMainController InstantiateEnemy(Vector3 enemyPosition, int ID)
        {
            foreach (var t in _enemyesPull.Enemyes.Where(t => t.IsActive == false))
            {
                t.gameObject.SetActive(true);
                t.gameObject.transform.position = enemyPosition;
                return t;
            }
            
            var enemy = _factoryEnemy.Create(ID);
            enemy.gameObject.transform.SetParent(_enemyesPull.gameObject.transform,false);
            enemy.gameObject.transform.position = enemyPosition;
            _enemyesPull.Enemyes.Add(enemy);
            
            var movePointer = _factoryMovePointer.Create();
            movePointer.gameObject.transform.SetParent(_movePointerPull.gameObject.transform,false);
            movePointer.gameObject.transform.position = enemyPosition;
            _movePointerPull.PositionPointers.Add(movePointer.gameObject);

            enemy.EnemyController.MovePointer = movePointer.gameObject;
            return enemy;
        }
        
        public LevelController InstantiateLevel(int ID)
        {
            foreach (var t in _levelPull.Levels.Where(t => t.Id == ID))
            {
                return t;
            }
            
            var level = _factoryLevel.Create(ID);
            level.gameObject.transform.SetParent(_levelPull.gameObject.transform,false);
            _levelPull.Levels.Add(level);
            return level;
        }

    }
}