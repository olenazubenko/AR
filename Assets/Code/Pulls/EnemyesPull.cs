using System.Collections.Generic;
using UnityEngine;

namespace Code.Pulls
{
    public class EnemyesPull : MonoBehaviour
    {
        private List<EnemyMainController> _enemyes = new List<EnemyMainController>();
        
        public List<EnemyMainController> Enemyes => _enemyes;
    }
}