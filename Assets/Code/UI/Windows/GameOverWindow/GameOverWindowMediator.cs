using System;
using Managers;

namespace Code.Windows.StartWindow
{
    public class GameOverWindowMediator :BaseWindowMediator<GameOverWindowView, WindowData>
    {
       
        // private readonly OneListener<WindowType> _preCloseListener = new OneListener<WindowType>();
        //
        // public event Action<WindowType> PRE_CLOSE
        // {
        //     add { _preCloseListener.Add(value); }
        //     remove { _preCloseListener.Remove(value); }
        // }

       
        protected override void ShowStart()
        {
            base.ShowStart();
            Target.StartButton.onClick.AddListener(OnStartGame);
        }

        private void OnStartGame()
        {
            //_startGame.SafeInvoke(GameStates.Playing);
            Target.StartButton.onClick.RemoveListener(OnStartGame);
            //_gameManager.ShangeState(GameStates.Playing); 
            ManagerContext.UiManagers.Close(AfterClose);

        }
        
        private void AfterClose()
        {
            ManagerContext.GameManager.ShangeState(GameStates.WaitingToStart);
        }
    }
}