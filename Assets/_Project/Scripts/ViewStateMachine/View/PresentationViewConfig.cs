using System;
using Project.Scripts.View.Swipe;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.View
{
    [CreateAssetMenu(fileName = nameof(PresentationViewConfig), menuName = "Configs/View/PresentationViewConfig")]
    public class PresentationViewConfig : ScriptableObject
    {
        [field: SerializeField] public Slide SlidePrefab { get; private set; }
        [SerializeField] private ViewData[] viewData;

        public Sprite[] GetSpritesByType(ViewType viewType)
        {
            foreach (var data in viewData)
            {
                if (data._viewType == viewType)
                {
                    return data.Slides;
                }
            }

            return null;
        }
    }

    [Serializable]
    public struct ViewData
    {
        [FormerlySerializedAs("_buttonType")] [FormerlySerializedAs("ViewType")] public ViewType _viewType;
        public Sprite[] Slides;
    }
}