using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.View
{
    public interface ICustomButton
    {
        event Action<ViewType> ClickType;
        ViewType ViewType { get; }
    }
    
    public class CustomButton : MonoBehaviour, ICustomButton, IPointerDownHandler
    {
        public event Action Click;
        public event Action<ViewType> ClickType;

        [SerializeField] private CustomButtonConfig _config;

        private Transform _transform;
        private Tweener _tweenerScale;

        [field: SerializeField] public ViewType ViewType { get; private set; }

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
            
            _tweenerScale = _transform.DOScale(
                    _config.ScaleAnimationValue, 
                    _config.ScaleDuration)
                .SetEase(Ease.Linear)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() =>
                {
                    _tweenerScale = null;
                    ClickType?.Invoke(ViewType);
                    Click?.Invoke();
                });
        }
    }
}