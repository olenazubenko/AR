using UnityEngine;

[System.Serializable]
public class PlayerController : BaseController
{
    
    // Update is called once per frame
    [SerializeField] private float  _X = 0;
    [SerializeField] private float  _Y = 6;
    [SerializeField] private float  _Z = -8;

    private Camera _mainCamera;
    private bool isMove = false;
    
    [SerializeField] private GameObject  go;

    public override void Init(MainController mainController)
    {
        base.Init(mainController);
        _mainCamera = Camera.main;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                go.SetActive(true);
                isMove = true;
                MainController.MoveController.MoveToPoint(hit.point);
                go.transform.position = hit.point;
            }
        }
    }

    public void FixedUpdate()
    {
        _mainCamera.transform.position = new Vector3(MainController.transform.position.x+_X, 
            MainController.transform.position.y+_Y,
            MainController.transform.position.z+_Z);
        
        var keyPressedL = Input.GetKey("a");
        var keyPressedR = Input.GetKey("d");
        //var keyPressedZ = Input.GetKey("z");

        go.transform.rotation = MainController.transform.rotation;
        
        if ( keyPressedL && !isMove)
        {
            //go.SetActive(true);
            go.transform.position = MainController.transform.position;
            go.transform.Translate(Vector3.left);
            
            MainController.MoveController.MoveToPoint(go.transform.position, MoveType.left);
            return;
        }
        if ( keyPressedR && !isMove)
        {
            //go.SetActive(true);
            go.transform.position = MainController.transform.position;
            go.transform.Translate(Vector3.right);
            
            MainController.MoveController.MoveToPoint(go.transform.position, MoveType.right);
            return;
        }
        
        // if ( keyPressedZ)
        // {
        //     MainController.AnimationController.IsDie = true;
        //     return;
        // }

        MainController.AnimationController.IsShoot = false;
        MainController.AnimationController.IsDie = false;


        if ((go.transform.position - MainController.transform.position).magnitude < 0.5F )
        {
            go.SetActive(false);
            isMove = false;
        }
    }
}
