using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
[System.Serializable]
public class AnimationController:BaseController
{
    // Start is called before the first frame update
    [SerializeField] private float _shootAnimation = 0.5F;
    private Animator _animatoir;
    private const string IS_WALKING = "isWalking";
    private const string IS_LEFT = "isLeft";
    private const string IS_RIGHT = "isRight";
    private const string IS_SOOT = "isSoot";
    private const string IS_DIE = "isDie";
    private const string IS_IDLE = "isIdle";
    
    private  bool _isWalk = false;
    private  bool _isLeft = false;
    private  bool _isRight = false;
    private  bool _isShoot = false;
    private  bool _isDie = false;
    private  bool _isIdle = false;
    
    private  bool _isShootAnimationPlayed = false;

    public bool IsWalk
    {
        get
        {
            return _isWalk;
        }
        set
        {
            _isWalk = value;
        }
    }
    
    public bool IsLeft
    {
        get
        {
            return _isLeft;
        }
        set
        {
            _isLeft = value;
        }
    }
    
    public bool IsRight
    {
        get
        {
            return _isRight;
        }
        set
        {
            _isRight = value;
        }
    }
    
    public bool IsShoot
    {
        get
        {
            return _isShoot;
        }
        set
        {
            _isShoot = value;
        }
    }
    
    public bool IsShootAnimationPlayed
    {
        get
        {
            return _isShootAnimationPlayed;
        }
        set
        {
            _isShootAnimationPlayed = value;
        }
    }
    
    public bool IsDie
    {
        get
        {
            return _isDie;
        }
        set
        {
            _isDie = value;
        }
    }
    
    public bool IsIdle
    {
        get
        {
            return _isIdle;
        }
        set
        {
            _isIdle = value;
        }
    }

    public override void Init(MainController mainController)
    {
        base.Init(mainController);
        _animatoir = MainController.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (IsShoot)return;
       
        if (IsWalk)
        {
            _animatoir.Play("Rifle_Walk");
            _animatoir.SetBool(IS_WALKING, true);
            return;
        }
        if (IsLeft)
        {
            _animatoir.Play("Strafe_Left");
            _animatoir.SetBool(IS_LEFT, true);
            return;
        }
        if (IsRight)
        {
            _animatoir.Play("Strafe_Right");
            _animatoir.SetBool(IS_RIGHT, true);
            return;
        }

        _animatoir.SetBool(IS_WALKING, false);
        _animatoir.SetBool(IS_LEFT, false);
        _animatoir.SetBool(IS_RIGHT, false);
    }

    public void PlayShoot()
    {
        _animatoir.Rebind();
        _animatoir.Play("Rifle_SingleShot");
        //_animatoir.SetBool(IS_SOOT, true);
        // _isShootAnimationPlayed = true;
        // DOVirtual.DelayedCall(_shootAnimation, () => ShootFinish());
    }

    public void ShootFinish()
    {
        Debug.Log("REDY");
        //_isShootAnimationPlayed = false;
    }

    public void PlayDie()
    {
        _animatoir.Rebind();
        _animatoir.Play("Collapse_GutShot");
        //_animatoir.SetBool(IS_DIE, true);
    }

    public void SetSpeed(float speed)
    {
        _animatoir.speed = speed;
    }

    public void ResetAnimation()
    {
       if(_animatoir == null) return;
        _animatoir.Rebind();
        _animatoir.Play("Rifle_Idle_1");
        //_animatoir.SetBool(IS_IDLE, true);
    }
}
