using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseController
{
    private MainController _mainController;
    public virtual MainController MainController => _mainController;
    public  virtual void Init(MainController mainController)
    {
        _mainController = mainController;
    }
}
