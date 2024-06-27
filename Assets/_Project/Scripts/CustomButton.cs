using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.View
{
    public interface ICustomButton
    {
        event Action<ViewType> ClickType;
        event Action Click;
        ViewType ViewType { get; }
        void SetDisplay(bool isVisible);
    }
    
    public class CustomButton : MonoBehaviour, ICustomButton, IPointerDownHandler
    {
        public event Action<ViewType> ClickType;
        public event Action Click;

        [SerializeField] private ViewType _viewType;
        [SerializeField] private float _scaleDuration = 0.2f;
        [SerializeField] private float _scaleAnimationValue = 0.9f;

        private Transform _transform;
        private Tweener _tweenerScale;
        
        public ViewType ViewType => _viewType;
        
        public void SetDisplay(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void OnDestroy()
        {
            ClickType = null;
            Click = null;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_tweenerScale != null)
            {
                return;
            }
            
            _tweenerScale = _transform.DOScale(_scaleAnimationValue, _scaleDuration)
                .SetEase(Ease.Linear)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() =>
                {
                    _tweenerScale = null;
                    ClickType?.Invoke(_viewType);
                    Click?.Invoke();
                });
        }
    }
}