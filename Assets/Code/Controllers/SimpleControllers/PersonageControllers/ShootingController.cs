using System.Collections;
using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class ShootingController : BaseController
{
    [SerializeField] private float _frequency = 3;
    [SerializeField] private float _shootPower = 1;
    
    private bool isShoot = false;
    public float Frequency
    {
        get => _frequency;
        set => _frequency = value;
    }
    
    public float ShootPower
    {
        get => _shootPower;
        set => _shootPower = value;
    }

    public bool MakeShoot => !isShoot;

    public void Shoot(Transform shootPosition)
    {
        isShoot = true;
        MainController.AnimationController.IsShoot = true;
        MainController.AnimationController.PlayShoot();
        var bullet = MainController.ManagerContext.InstantiateManagers.InstantiateBullet(shootPosition);
        bullet.IsUsed = true;
        bullet.Damage = ShootPower;
        MainController.StartCoroutine(ShootFinish());
    }

    private IEnumerator ShootFinish()
    {
        yield return new WaitForSeconds(Frequency);
        isShoot = false;
    }
   
}
