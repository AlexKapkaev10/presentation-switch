using Project.Scripts.View.Swipe;
using UnityEngine;

namespace Project.Scripts.View
{
    public class PresentationView : View
    {
        [SerializeField] private PresentationViewConfig _config;
        [SerializeField] private RectTransform _slideParent;
        [SerializeField] private SwipeSnapMenu _swipeSnapMenu;
        
        public override void Initialize(ViewType viewType)
        {
            foreach (var sprite in _config.GetSpritesByType(viewType))
            {
                var slide = Instantiate(_config.SlidePrefab, _slideParent);
                
                slide.SetSprite(sprite);
            }
            
            _swipeSnapMenu.Initialize();
        }
    }
}