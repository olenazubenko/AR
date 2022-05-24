using System;
using System.Collections.Generic;
using Code.Bullet;
using UnityEngine;

namespace Code.Pulls
{
    public class BulletsPull : MonoBehaviour
    {
        private List<BulletController> _bullets = new List<BulletController>();
        
        public List<BulletController> Bullets => _bullets;
    }
}