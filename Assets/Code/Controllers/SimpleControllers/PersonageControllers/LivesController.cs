using System;
using UnityEngine;

namespace Code.Controllers
{
    [System.Serializable]
    public class LivesController  :BaseController
    {
        [SerializeField] private float  _livesCount = 3;

        private float _currentLivesCount;
        private bool  _isDie;
        
        private readonly OneListener<float> _livesCountListener = new OneListener<float>();
        
        public event Action<float> LivesCountAction
        {
            add { _livesCountListener.Add(value); }
            remove { _livesCountListener.Remove(value); }
        }
        public bool IsDie
        {
            get => _isDie;
            set => _isDie = value;
        } 
        
        public float LivesCount
        {
            get => _currentLivesCount;
            set => _currentLivesCount = value;
        }

        public void ReInit()
        {
            IsDie = false;
            _currentLivesCount = _livesCount;
        }
        
        public void OnCollisionEnter(Collision collision)
        {
            var contact = collision.contacts[0];
            if (collision.collider.name == "Bullet" && !IsDie)
            {
                var damage =collision.collider.GetComponent<BulletController>().Damage;
                _currentLivesCount -= damage;
                _livesCountListener.SafeInvoke(_currentLivesCount);
                if (!(_currentLivesCount <= 0)) return;
                MainController.AnimationController.PlayDie();
                MainController.MoveController.StopMoving();
                IsDie = true;
            }
        }
    }
}