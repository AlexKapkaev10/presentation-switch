using UnityEngine;

namespace Project.Scripts.View
{
    public class ViewStateMachine : MonoBehaviour
    {
        [SerializeField] private ViewStateMachineConfig _config;

        private IView _currentView;
        
        private void Awake()
        {
            SwitchView(ViewType.Menu);
        }
        
        private void SwitchView(ViewType type)
        {
            if (_currentView != null)
            {
                _currentView.SwitchView -= SwitchView;
                _currentView.SetVisible(false);
                _currentView = null;
            }

            var prefab = _config.GetViewPrefabByType(type);

            if (!prefab)
            {
                return;
            }
            
            _currentView = Instantiate(prefab, null);
            _currentView.SwitchView += SwitchView;
            if (type == ViewType.Menu)
            {
                return;
            }
            
            _currentView.Initialize(type);
        }
    }
}