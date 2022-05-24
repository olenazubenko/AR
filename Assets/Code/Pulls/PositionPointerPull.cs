using System;
using System.Collections.Generic;
using Code.Bullet;
using UnityEngine;

namespace Code.Pulls
{
    public class PositionPointerPull : MonoBehaviour
    {
        private List<GameObject> _positionPointers = new List<GameObject>();
        
        public List<GameObject> PositionPointers => _positionPointers;
    }
}