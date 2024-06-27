using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.View.Swipe
{
    public class SwipeSnapMenu : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public event Action<int> TabSelected;
        public event Action<int> TabSnapped;
        
        [SerializeField] private RectTransform _contentContainer;
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private float _snapSpeed = 15f;

        [SerializeField] private CustomButton _nextButton;
        [SerializeField] private CustomButton _previousButton;
        
        private readonly List<float> _itemPositionsNormalized = new List<float>();
        private float _targetScrollBarValueNormalized = 0;
        private float _itemSizeNormalize;
        private int _selectedTabIndex;
        
        private bool _isDragging;
        private bool _isSnapping;

        public void Initialize()
        {
            _nextButton.SetDisplay(false);
            _nextButton.SetDisplay(false);

            _nextButton.Click += SlideNext;
            _previousButton.Click += SlidePrevious;

            Recalculate();
        }

        private void OnDestroy()
        {
            _nextButton.Click -= SlideNext;
            _previousButton.Click -= SlidePrevious;
        }

        private void Update()
        {
            if (_isDragging)
            {
                return;
            }

            if (_isSnapping)
            {
                SnapContent();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _isSnapping = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _targetScrollBarValueNormalized = _scrollbar.value;
            _isDragging = false;
            _isSnapping = true;
            
            FindSnappingTabAndStartSnapping();
        }

        private void SlideNext()
        {
            var index = _selectedTabIndex;
            
            SelectTab(index + 1);
        }

        private void SlidePrevious()
        {
            var index = _selectedTabIndex;
            
            SelectTab(index - 1);
        }

        private void Recalculate()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_contentContainer);
            
            _itemPositionsNormalized.Clear();

            var itemsCount = _contentContainer.childCount;
            _itemSizeNormalize = 1f / (itemsCount - 1);

            for (int i = 0; i < itemsCount; i++)
            {
                var itemPositionNormalized = _itemSizeNormalize * i;
                
                _itemPositionsNormalized.Add(itemPositionNormalized);
            }
            
            SelectTab(_selectedTabIndex);
        }

        private void SelectTab(int tabIndex)
        {
            if (tabIndex < 0 || tabIndex >= _itemPositionsNormalized.Count)
            {
                return;
            }

            _selectedTabIndex = tabIndex;
            _targetScrollBarValueNormalized = _itemPositionsNormalized[tabIndex];
            _isSnapping = true;

            _previousButton.SetDisplay(_selectedTabIndex != 0);
            _nextButton.SetDisplay(_selectedTabIndex != _itemPositionsNormalized.Count - 1);

            TabSelected?.Invoke(tabIndex);
        }

        private void FindSnappingTabAndStartSnapping()
        {
            for (int i = 0; i < _itemPositionsNormalized.Count; i++)
            {
                var itemPositionNormalize = _itemPositionsNormalized[i];

                if (_targetScrollBarValueNormalized < itemPositionNormalize
                    + _itemSizeNormalize / 2f
                    && _targetScrollBarValueNormalized > itemPositionNormalize
                    - _itemSizeNormalize / 2f)
                {
                    SelectTab(i);
                    break;
                }
            }
        }

        private void SnapContent()
        {
            if (_itemPositionsNormalized.Count < 2)
            {
                _isSnapping = false;
                return;
            }

            var targetPosition = _itemPositionsNormalized[_selectedTabIndex];

            _scrollbar.value = Mathf.Lerp(_scrollbar.value, targetPosition, Time.deltaTime * _snapSpeed);

            if (Mathf.Abs(_scrollbar.value - targetPosition) <= 0.0001f)
            {
                _isSnapping = false;
                TabSnapped?.Invoke(_selectedTabIndex);
            }
        }
    }
}