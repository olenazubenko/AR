using Managers;
using UnityEngine;
using Zenject;

namespace Code.Controllers
{
    public class Controller : MonoBehaviour
    {
        [Inject] private ManagerContext _managerContext;
        public ManagerContext ManagerContext => _managerContext;
    }
}