using UnityEngine;

namespace Code.Controllers.SimpleControllers.PersonageControllers
{
    [System.Serializable]
    public class EnemyСaptureController: СaptureController
    {
        private EnemyMainController _mainController;
        
        public EnemyMainController MainController => _mainController;
        
        private bool _playerСapture;
        
        public bool PlayerСapture => _playerСapture;
        
        public void Init(EnemyMainController mainController)
        {
            _mainController = mainController;
        }
        
        public void Update()
        {
            var offset = MainController.PlayerMainController.transform.position - MainController.transform.position;
            if (offset.sqrMagnitude <= Distance && 
                !MainController.PlayerMainController.LivesController.IsDie  
                )
            {
                _playerСapture = true;
            }
            else
            {
                _playerСapture = false;
            }
            
        }

        public void PrepearSooting()
        {
            var offset = MainController.PlayerMainController.transform.position - MainController.transform.position;
            
            var singleStep = RotationSpeed * Time.deltaTime;
            var newDirection = Vector3.RotateTowards(MainController.transform.forward, offset, singleStep, 0.0f);
            MainController.transform.rotation = Quaternion.LookRotation(newDirection);
            
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
                
            if (hit.collider.tag == "Player")
            {
                //MainController.MoveController.StopMoving();
                MainController.ShootingController.Shoot(ShootPosition);
            }
        }
        
    }
}