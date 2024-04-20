using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Espale.Utilities.Components
{
    [RequireComponent(typeof(Light2D)), ExecuteInEditMode]
    public class Light2DSpriteMatcher : MonoBehaviour
    {
        public SpriteRenderer target;
        private Light2D light2D;
    
        // This allows us to bypass the private light sprite
        private readonly FieldInfo lightCookieSprite = typeof( Light2D ).GetField( "m_LightCookieSprite", BindingFlags.NonPublic | BindingFlags.Instance);

        private void Awake() => light2D = GetComponent<Light2D>();
        private void LateUpdate()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && !light2D)
                light2D = GetComponent<Light2D>();
#endif
            lightCookieSprite.SetValue(light2D, target.sprite);
        }
    }
}
