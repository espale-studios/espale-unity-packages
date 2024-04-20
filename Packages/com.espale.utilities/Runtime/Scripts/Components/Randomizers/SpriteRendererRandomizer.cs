using NaughtyAttributes;
using UnityEngine;

namespace Espale.Utilities.Components.Randomizers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererRandomizer : MonoBehaviour
    {
        public bool randomizeOnStart = true;
        
        [Header("Randomization")]
        public bool randomizeSprite;
        [ShowIf(nameof(randomizeSprite))] public Sprite[] spriteOptions = new Sprite[2];
        
        public bool randomizeColor;
        [ShowIf(nameof(randomizeColor))] public Gradient colorRange;
        
        private SpriteRenderer spriteRenderer;

        private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

        private void Start()
        {
            if (randomizeOnStart) Randomize();
        }

        public void Randomize()
        {
            if (randomizeSprite) spriteRenderer.sprite = CollectionUtilities.GetRandomItemFrom(spriteOptions);
            if (randomizeColor) spriteRenderer.color = colorRange.Evaluate(Random.Range(0f, 1f));
        }
    } 
}
