using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RayCaster:BaseController
{
    [SerializeField] private Transform _transform;
    
    public Transform ShootPosition {
        get => _transform;
        set => _transform = value;
    }

    // Update is called once per frame
    public void Update()
    { 
        // RaycastHit hit;
        // Ray ray = new Ray(_transform.position, _transform.forward);
        // var isHit = Physics.Raycast(ray, out hit, MainController.ShootingController.Distance);
        //
        // if (isHit)
        // {
        //     Debug.Log(hit.collider.tag + ' ' + hit.distance);
        //     
        //     
        //     var lineObj = new GameObject();
        //     var line = lineObj.AddComponent<LineRenderer>();
        //     line.SetPosition(0, _transform.position);
        //     line.SetPosition(1, new Vector3(hit.transform.position.x, _transform.position.y, hit.transform.position.z));
        //     line.SetWidth(0.03f, 0.03f);
        //     MainController.Destroy(lineObj, 0.5f);
        //     
        //     if (hit.collider.tag == "Enemy" &&  
        //         MainController.ShootingController.MakeShoot && 
        //         !MainController.LivesController.IsDie &&
        //         !hit.collider.GetComponent<MainController>().LivesController.IsDie
        //         )
        //     {
        //         MainController.MoveController.StopMoving();
        //         MainController.ShootingController.Shoot();
        //         
        //     }
        // }
    }
}
