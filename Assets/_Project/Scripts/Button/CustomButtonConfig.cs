using UnityEngine;

namespace Project.Scripts.View
{
    [CreateAssetMenu(fileName = nameof(CustomButtonConfig), menuName = "Configs/View/CustomButtonConfig")]
    public class CustomButtonConfig : ScriptableObject
    {
        [field: SerializeField] public float ScaleDuration { get; private set; } = 0.2f;
        [field: SerializeField] public float ScaleAnimationValue { get; private set; } = 0.9f;
    }
}