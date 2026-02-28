using NaughtyAttributes;
using UnityEngine;

namespace Espale.Utilities.Components
{
    [RequireComponent(typeof(LineRenderer))]
    public class AnimationCurveRenderer : MonoBehaviour
    {
        [Header("Positioning")]
        public Vector3 startPosition;
        public Vector3 endPosition = Vector3.right;
        public bool useGlobalPosition = false;
    
        [Header("Shake")] 
        [CurveRange(0f, -1f, 1f, 1f)] public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 0f);
        public float magnitude = 1f;
    
        [Header("Rendering")] 
        public bool autoDrawOnUpdate = true;
        [SerializeField, Range(0f, 1f)] public float t = 1;
        [SerializeField, MinValue(2)] private int resolution = 50;
        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        
        private void Update()
        {
            t = Mathf.Clamp01(t);
            if (autoDrawOnUpdate) DrawCurve();
        }
    
        /// <summary>
        /// Updates the LineRenderer component based on the value of the curve and t.
        /// </summary>
        public void DrawCurve()
        {
            if (t <= 0)
            {
                lineRenderer.positionCount = 0;
                return;
            }
            
            lineRenderer.positionCount = resolution + 1;
            for (var i = 0; i <= resolution; i++)
            {
                var r = i * t / resolution;
                lineRenderer.SetPosition(i, GetLinePosAt(r));
            }
        }
    
        /// <summary>
        /// Returns the world position of the line at value t, where 0 is the startPosition and 1 is the endPosition.
        /// </summary>
        /// <param name="t">Line position ratio, where 0 is the startPosition and 1 is the endPosition.</param>
        /// <returns>World Position at t</returns>
        public Vector3 GetLinePosAt(float t)
        {
            t = Mathf.Clamp01(t);
            
            var point = Vector3.Lerp(GetStartPos(), GetEndPos(), t);
            
            var shakeMag = curve.Evaluate(t) * magnitude;
            var shakeVector = Vector3.Cross(GetEndPos() - GetStartPos(), Vector3.forward).normalized * shakeMag;
            
            return point + shakeVector;
        }
        
        private Vector3 GetStartPos() => useGlobalPosition ? startPosition : (transform.position + startPosition);
        private Vector3 GetEndPos() => useGlobalPosition ? endPosition : (transform.position + endPosition);
    
        private void OnDrawGizmosSelected()
        {
            for (float i = 0; i < resolution; i++)
            {
                var endRatio = (i + 1) / resolution;
                if (endRatio > t) endRatio = t;
                Gizmos.DrawLine(GetLinePosAt(i / resolution), GetLinePosAt(endRatio));
    
                if (endRatio >= t) break;
            }
        }
    }
}
