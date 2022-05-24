using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Windows.StartWindow
{
    public class LevelLoseWindowView : BaseWindowView
    {
        [Header("Panel Start Conteyner")]
        [SerializeField] private GameObject  _panelStart;
        [SerializeField] private Button  _startButton;
        [Space]
        [SerializeField] private TextMeshProUGUI  _NextLevel;
        [Space]
        [SerializeField] private TextMeshProUGUI  _LifeInLevel;
        [SerializeField] private Button  _LifeInLevelButton;
        [SerializeField] private TextMeshProUGUI  _LifeInLevelButtonText;
        [Space]
        [SerializeField] private TextMeshProUGUI  _SootingPower;
        [SerializeField] private Button  _SootingPowerButton;
        [SerializeField] private TextMeshProUGUI  _SootingPowerButtonText;
        [Space]
        [SerializeField] private TextMeshProUGUI  _SootingRange;
        [SerializeField] private Button  _SootingRangeButton;
        [SerializeField] private TextMeshProUGUI  _SootingRangeButtonText;
        [Space]
        [SerializeField] private TextMeshProUGUI  _ReloadSpeed;
        [SerializeField] private Button  _ReloadSpeedButton;
        [SerializeField] private TextMeshProUGUI  _ReloadSpeedButtonText;
        [Space]
        [Header("Panel No Money Conteyner")]
        [SerializeField] private GameObject  _panelNoMoney;
        [SerializeField] private Button  _getMoreMoneyButton;
        [SerializeField] private Button  _cancelButton;
        [SerializeField] private TextMeshProUGUI  _needMoreMoneyText;
        
        public GameObject PanelStart
        {
            get { return _panelStart; }
        }
        
        protected override void CreateMediator()
        {
            _mediator = new LevelLoseWindowMediator();
        }

        public Button StartButton
        {
            get { return _startButton; }
        }

        public Button LifeInLevelButton
        {
            get { return _LifeInLevelButton; }
        }
        
        public Button SootingPowerButton
        {
            get { return _SootingPowerButton; }
        }
        
        public Button SootingRangeButton
        {
            get { return _SootingRangeButton; }
        }
        
        public Button ReloadSpeedButton
        {
            get { return _ReloadSpeedButton; }
        }

        public string NextLevel
        {
            get { return _NextLevel.text; }
            set { _NextLevel.text = value; }
        }

        public string LifeInLevel
        {
            get { return _LifeInLevel.text; }
            set { _LifeInLevel.text = value; }
        }
        
        public string LifeInLevelButtonText
        {
            get { return _LifeInLevelButtonText.text; }
            set { _LifeInLevelButtonText.text = value; }
        }
        
        public string SootingPower
        {
            get { return _SootingPower.text; }
            set { _SootingPower.text = value; }
        }
        
        public string SootingPowerButtonText
        {
            get { return _SootingPowerButtonText.text; }
            set { _SootingPowerButtonText.text = value; }
        }
        
        public string SootingRange
        {
            get { return _SootingRange.text; }
            set { _SootingRange.text = value; }
        }
        
        public string SootingRangeButtonText
        {
            get { return _SootingRangeButtonText.text; }
            set { _SootingRangeButtonText.text = value; }
        }
        
        public string ReloadSpeed
        {
            get { return _ReloadSpeed.text; }
            set { _ReloadSpeed.text = value; }
        }
        
        public string ReloadSpeedButtonText
        {
            get { return _ReloadSpeedButtonText.text; }
            set { _ReloadSpeedButtonText.text = value; }
        }
        
        public GameObject PanelNoMoney
        {
            get { return _panelNoMoney; }
        }
        
        public Button GetMoreMoneyButton
        {
            get { return _getMoreMoneyButton; }
        }
        
        public Button CancelButton
        {
            get { return _cancelButton; }
        }
        
        public string NeedMoreMoneyText
        {
            get { return _needMoreMoneyText.text; }
            set { _needMoreMoneyText.text = value; }
        }
    }
}