using System;
using Code.Controllers;
using UnityEngine;

public class MainController : Controller
{
    [SerializeField] private AnimationController _animationController;
    //[SerializeField] private RayCaster _rayCaster;
    [SerializeField] private MoveController _moveController;
    [SerializeField] private ShootingController _shootingController;
    [SerializeField] private LivesController _livesController;
    
    public AnimationController AnimationController => _animationController;
    //public RayCaster RayCaster => _rayCaster;
    public MoveController MoveController => _moveController;
    public ShootingController ShootingController => _shootingController;
    public LivesController LivesController => _livesController;
    
    protected readonly OneListener <MainController>_personageIsDieListener = new OneListener<MainController>();
        
    public event Action<MainController> PersonageIsDieListener
    {
        add { _personageIsDieListener.Add(value); }
        remove { _personageIsDieListener.Remove(value); }
    }
    

    public bool IsActive { get; private set; } 
    
    protected void Start()
    {
        AnimationController.Init(this);
        //RayCaster.Init(this);
        MoveController.Init(this);
        ShootingController.Init(this);
        LivesController.Init(this);
        LivesController.LivesCountAction += PersonageLifeCount;
    }

    protected void Update()
    {
        AnimationController.Update();
        //RayCaster.Update();
        MoveController.Update();
    }
    
    protected void OnCollisionEnter(Collision collision)
    {
        LivesController.OnCollisionEnter(collision);
    }

    protected void OnDestroy()
    {
        LivesController.LivesCountAction -= PersonageLifeCount;
    }

    protected virtual void PersonageLifeCount(float lifes)
    {
        if (lifes <= 0 )
        {
            IsActive = false;
            _personageIsDieListener.SafeInvoke(this);
        }
    }

    public bool SetActivePersonage
    {
        set
        {
            IsActive = value;
            if (!value) return;
            _livesController.ReInit();
            _animationController.ResetAnimation();
        }
    }
}
