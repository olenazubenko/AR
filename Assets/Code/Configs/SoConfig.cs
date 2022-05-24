using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "SoConfig", menuName = "SO Game Config", order = 0)]
    public class SoConfig : ScriptableObject
    {
        public  GameObject  BulletPrefab = null;
        public  GameObject  MovePointerPrefab = null;
        public  GameObject [] EnemyPrefab = null;
        public  GameObject [] LevelPrefab = null;
    }
}