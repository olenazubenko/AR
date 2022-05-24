using UnityEngine;
using Zenject;

namespace Core
{
    [CreateAssetMenu (fileName = "SoInstaller", menuName = "Create SO Installer")]
    public class SoInstaller : ScriptableObjectInstaller<SoInstaller>
    {
        [SerializeField] public SoConfig _soConfig;
        [SerializeField] public WindowsConfig _windowsConfig;
        [SerializeField] public PanelsConfig _panelConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(_soConfig).IfNotBound();
            Container.BindInstance(_windowsConfig).IfNotBound();
            Container.BindInstance(_panelConfig).IfNotBound();
        }
    }
}