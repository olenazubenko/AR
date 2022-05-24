using System.Linq;
using Code.Pulls;
using UnityEngine;
using Zenject;

namespace Code.Controllers.SimpleControllers.PersonageControllers
{
    [System.Serializable]

    public class PlayerCaptureController: СaptureController
    {
        private PlayerMainController _mainController;
        
        public PlayerMainController MainController => _mainController;
        
        public  void Init(PlayerMainController mainController)
        {
            _mainController = mainController;
        }
        
        private GameObject _enemyCaptured;
        
        public GameObject EnemyCaptured => _enemyCaptured;
        
        public void Update()
        {
            if (MainController.MoveController.MoveType != MoveType.none)
            {
                _enemyCaptured = null;
                return;
            }
            foreach (var t in MainController.EnemyesPull.Enemyes.Where
                (t => t.IsActive && !t.LivesController.IsDie)
            )
            {
                var offset = t.transform.position - MainController.transform.position;
                if (offset.sqrMagnitude <= Distance)
                {
                    _enemyCaptured = t.gameObject;
                    var singleStep = RotationSpeed * Time.deltaTime;
                    var newDirection = Vector3.RotateTowards(MainController.transform.forward, offset, singleStep, 0.0f);
                    MainController.transform.rotation = Quaternion.LookRotation(newDirection);

                    
                    if (!MainController.ShootingController.MakeShoot || MainController.LivesController.IsDie) return;
                    RaycastHit hit;
                    var ray = new Ray(MainController.transform.position, MainController.transform.forward);
                    var isHit = Physics.Raycast(ray, out hit, Distance);
                    if (!isHit) return;
                    
                    // var lineObj = new GameObject();
                    // var line = lineObj.AddComponent<LineRenderer>();
                    // line.SetPosition(0, ShootPosition.position);
                    // line.SetPosition(1, new Vector3(hit.transform.position.x, ShootPosition.position.y, hit.transform.position.z));
                    // line.SetWidth(0.03f, 0.03f);
                    // PlayerMainController.Destroy(lineObj, 0.5f);
                    
                    if (hit.collider.tag == "Enemy" && !hit.collider.GetComponent<MainController>().LivesController.IsDie)
                    {
                        //MainController.MoveController.StopMoving();
                        MainController.ShootingController.Shoot(ShootPosition);
                    }
                    return;
                }
            }

            _enemyCaptured = null;
        }    
    }
}