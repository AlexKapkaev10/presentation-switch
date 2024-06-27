using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.View.Swipe
{
    public interface ISlide
    {
        void SetSprite(in Sprite sprite);
    }
    
    public class Slide : MonoBehaviour, ISlide
    {
        [SerializeField] private Image _image;


        public void SetSprite(in Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}