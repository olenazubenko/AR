using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Pulls
{
    public class LevelPull : MonoBehaviour
    {
        private List<LevelController> _levels = new List<LevelController>();
        public List<LevelController> Levels => _levels;

        public LevelController GetCurrentLevel()
        {
            return Levels.Where(l => l.IsCurrentLevel).FirstOrDefault();
        }
    }
}