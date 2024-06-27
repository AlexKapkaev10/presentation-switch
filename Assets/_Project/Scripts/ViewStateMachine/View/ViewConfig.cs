using System;
using Project.Scripts.View.Swipe;
using UnityEngine;

namespace Project.Scripts.View
{
    [CreateAssetMenu(fileName = nameof(ViewConfig), menuName = "Configs/View/ViewConfig")]
    public class ViewConfig : ScriptableObject
    {
        [field: SerializeField] public Slide SlidePrefab { get; private set; }
        [SerializeField] private ViewData[] viewData;

        public Sprite[] GetSpritesByType(ViewType viewType)
        {
            foreach (var data in viewData)
            {
                if (data.ViewType == viewType)
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
        public ViewType ViewType;
        public Sprite[] Slides;
    }
}