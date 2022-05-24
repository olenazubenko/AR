using System;
using UnityEngine;

namespace Code
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float  _speed = 3;
        private float _damage = 1;

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }
        
        private bool isUsed = false;

        public bool IsUsed
        {
            get => isUsed;
            set => isUsed = value;
        }

        private void Update()
        {
            if (isUsed)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            var contact = collision.contacts[0];
            isUsed = false;
            gameObject.SetActive(false);
        }
    }
}