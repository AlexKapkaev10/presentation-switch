using System;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.View
{
    public interface IView
    {
        event Action<ViewType> SwitchView;
        void Initialize(ViewType viewType);
        void SetVisible(bool isVisible, float duration = 0.2f);
    }
    
    public class View : MonoBehaviour, IView
    {
        public event Action<ViewType> SwitchView;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private ICustomButton[] _customButtons;
        
        private void OnEnable()
        {
            _customButtons = GetComponentsInChildren<ICustomButton>();

            foreach (var button in _customButtons)
            {
                if (button.ViewType == ViewType.None)
                {
                    continue;
                }
                
                button.ClickType += OnSwitchViewClick;
            }
        }

        private void OnDisable()
        {
            foreach (var button in _customButtons)
            {
                if (button.ViewType == ViewType.None)
                {
                    continue;
                }
                
                button.ClickType -= OnSwitchViewClick;
            }
        }

        private void Awake()
        {
            SetVisible(true);
        }

        private void OnDestroy()
        {
            SwitchView = null;
        }

        private void OnSwitchViewClick(ViewType type)
        {
            SwitchView?.Invoke(type);
        }

        public virtual void Initialize(ViewType viewType)
        {
        }

        public void SetVisible(bool isVisible, float duration = 0.2f)
        {
            _canvasGroup.interactable = isVisible;
            _canvasGroup.blocksRaycasts = isVisible;
            
            _canvasGroup.DOFade(isVisible ? 1 : 0, duration)
                .From(isVisible ? 0 : 1)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    if (isVisible)
                        return;
                    
                    Destroy(gameObject);
                });
        }
    }
    
    public enum ViewType
    {
        None = -1,
        Menu = 0,
        Presentation1 = 1,
        Presentation2 = 2,
        Presentation3 = 3,
        Presentation4 = 4,
        Presentation5 = 5,
        Presentation6 = 6,
        Presentation7 = 7,
    }
}