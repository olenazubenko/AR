using Code.Windows;
using TMPro;
using UnityEngine;

namespace Code.Panels
{
    public class MainUiPanelView : BasePanelView
    {
        [SerializeField] private TextMeshProUGUI  _Money;
        [SerializeField] private TextMeshProUGUI  _Life;
        [SerializeField] private TextMeshProUGUI  _Enemy;
        [SerializeField] private TextMeshProUGUI  _Reload;
        [SerializeField] private TextMeshProUGUI  _XP;
        [SerializeField] private TextMeshProUGUI  _Power;
        [SerializeField] private TextMeshProUGUI  _Armor;
        [SerializeField] private TextMeshProUGUI  _Range;
        
        protected override void CreateMediator()
        {
            _mediator = new MainPanelMediator();
        }
        
        public string Money
        {
            get { return _Money.text; }
            set { _Money.text = value; }
        }
        
        public string Life
        {
            get { return _Life.text; }
            set { _Life.text = value; }
        }
        
        public string Power
        {
            get { return _Power.text; }
            set { _Power.text = value; }
        }
        
        public string Enemy
        {
            get { return _Enemy.text; }
            set { _Enemy.text = value; }
        }
        
        public string Reload
        {
            get { return _Reload.text; }
            set { _Reload.text = value; }
        }
        
        public string XP
        {
            get { return _XP.text; }
            set { _XP.text = value; }
        }
        
        public string Armor
        {
            get { return _Armor.text; }
            set { _Armor.text = value; }
        }
        
        public string Range
        {
            get { return _Range.text; }
            set { _Range.text = value; }
        }
    }
}