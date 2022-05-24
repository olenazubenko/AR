using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Windows.StartWindow
{
    public class GameOverWindowView : BaseWindowView
    {
        [SerializeField] private Button  _startButton;
        
        protected override void CreateMediator()
        {
            _mediator = new GameOverWindowMediator();
        }

        public Button StartButton
        {
            get { return _startButton; }
        }
    }
}