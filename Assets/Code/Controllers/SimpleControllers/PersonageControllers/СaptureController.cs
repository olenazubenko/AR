using UnityEngine;

namespace Code.Controllers.SimpleControllers.PersonageControllers
{
    public class СaptureController:BaseController
    {
        [SerializeField] private float _distance = 15;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _rotationSpeed = 1.0f;
        
        public float Distance {
            get => _distance;
            set => _distance = value;
        }
        
        public float RotationSpeed => _rotationSpeed;

        public Transform ShootPosition {
            get => _transform;
            set => _transform = value;
        }
    }
}