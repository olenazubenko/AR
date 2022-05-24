using System;
using UnityEngine;
using UnityEngine.AI;

public enum MoveType
{
    forvard,
    left,
    right,
    none
}
[System.Serializable]
public class MoveController:BaseController
{
    // Start is called before the first frame update
    [SerializeField] private float  _angularSpeed = 1500;
    [SerializeField] private float  _moveSpeed = 1.5F;
    [SerializeField] private NavMeshAgent _agent;
    
    public NavMeshAgent Agent
    {
        get => _agent;
        set => _agent = value;
    } 
    private MoveType _moveType = MoveType.none;
    public MoveType MoveType => _moveType;
    
    private bool startMoving = false;

    public override void Init(MainController mainController)
    {
        base.Init(mainController);
        _agent.enabled = true;
        _agent.angularSpeed = _angularSpeed;
        _agent.speed = _moveSpeed;
    }

    public void Update()
    {
       
        if (_agent.velocity.magnitude != 0)
        {
            switch (_moveType)
            {
                case MoveType.forvard:
                    MainController.AnimationController.SetSpeed(_agent.velocity.magnitude);
                    MainController.AnimationController.IsWalk = true;
                    break;
                // case MoveType.left:
                //     MainController.AnimationController.SetSpeed(_agent.velocity.magnitude);
                //     MainController.AnimationController.IsLeft = true;
                //     break;
                // case MoveType.right:
                //     MainController.AnimationController.SetSpeed(_agent.velocity.magnitude);
                //     MainController.AnimationController.IsRight = true;
                //     break;
                case MoveType.none:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                
            }
            startMoving = false;
            return;
        }

        if (!startMoving)
        {
            _moveType = MoveType.none;
            MainController.AnimationController.IsWalk = false;
            MainController.AnimationController.IsLeft = false;
            MainController.AnimationController.IsRight = false;
            MainController.AnimationController.SetSpeed(1);
        }
    }

    public void MoveToPoint(Vector3 point, MoveType type = MoveType.forvard)
    {
        //if (MainController.AnimationController.IsShootAnimationPlayed) return;
        _moveType = type;
        startMoving = true;
        if (type == MoveType.forvard)
        {
            _agent.angularSpeed = _angularSpeed; 
        }
        else
        {
            _agent.angularSpeed = 0; 
        }
        _agent.SetDestination(point);
    }

    public void StopMoving()
    {
        
            _agent.SetDestination(MainController.gameObject.transform.position);
        
       
        
        _moveType = MoveType.none;
        MainController.AnimationController.IsWalk = false;
        MainController.AnimationController.IsLeft = false;
        MainController.AnimationController.IsRight = false;
        MainController.AnimationController.SetSpeed(1);
    }
}
