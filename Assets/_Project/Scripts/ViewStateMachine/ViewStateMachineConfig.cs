using UnityEngine;

namespace Project.Scripts.View
{
    [CreateAssetMenu(fileName = nameof(ViewStateMachineConfig), menuName = "Configs/View/ViewStateMachineConfig")]
    public class ViewStateMachineConfig : ScriptableObject
    {
        [SerializeField] private View _menuViewPrefab;
        [SerializeField] private View _presentationViewPrefab;

        public View GetViewPrefabByType(ViewType type)
        {
            return type == ViewType.Menu ? _menuViewPrefab : _presentationViewPrefab;
        }
    }
}